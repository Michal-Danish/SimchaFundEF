using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Simchas.Data
{
    public class SimchaRepository
    {
        private string _connectionString;
        public SimchaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Simcha> GetSimchas()
        {
            using (var context = new SimchaContext(_connectionString))
            {
                var simchas = context.Simchas.Include(s => s.Contributions).ToList();
                foreach(Simcha s in simchas)
                {
                    s.TotalAmount = context.Contributions.Where(c => c.SimchaId == s.Id).Sum(c => c.Amount);
                    s.TotalContributors = context.Contributions.Where(c => c.SimchaId == s.Id).Count();
                };
                return simchas;
            }
        }

        public int GetContributorCount()
        {
            using (var context = new SimchaContext(_connectionString))
            {
                return context.Contributors.Count();
            }
        }

        public void AddSimcha(Simcha s)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                context.Simchas.Add(s);
                context.SaveChanges();
            }
        }

        public IEnumerable<Contributor> GetContributors()
        {
            using (var context = new SimchaContext(_connectionString))
            {
                var contributors = context.Contributors.Include(c => c.Contributions).Include(c => c.Deposits).ToList();
                foreach(Contributor c in contributors)
                {
                    c.Balance = context.Deposits.Where(cd => cd.ContributorId == c.Id).Sum(cd => cd.Amount) -
                        context.Contributions.Where(cd => cd.ContributorId == c.Id).Sum(cd => cd.Amount);
                }
                return contributors;
            }
        }

        public void AddContributor(Contributor c, Deposit d)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                context.Contributors.Add(c);
                d.ContributorId = c.Id;
                context.Deposits.Add(d);
                context.SaveChanges();

            }
        }

        public void EditContributor(Contributor c)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand("UPDATE Contributors SET FirstName = @firstName, LastName = @lastName, CellNumber = @cellNumber " +
                                                    "WHERE Id = @id",
                    new SqlParameter("@firstName", c.FirstName),
                    new SqlParameter("@lastName", c.LastName),
                    new SqlParameter("@cellNumber", c.CellNumber),
                    new SqlParameter("@id", c.Id));
                context.SaveChanges();
            }
        }

        public void AddDeposit(Deposit d)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                context.Deposits.Add(d);
                context.SaveChanges();
            }
        }

        public Contributor GetContributor(int id)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                return context.Contributors.Include(c => c.Deposits).Include(c => c.Contributions).ThenInclude(c => c.Simcha).FirstOrDefault(c => c.Id == id);
            }
        }

        public Simcha GetSimcha(int id)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                return context.Simchas.FirstOrDefault(s => s.Id == id);
            }
        }

        public IEnumerable<Contributor> GetSimchaContributors(int simchaId)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                IEnumerable<Contributor> contributors = GetContributors();
                foreach(Contributor c in contributors)
                {
                    if(c.Contributions.FirstOrDefault(cn => cn.SimchaId == simchaId) != null)
                    {
                        c.AmountContributedToSimcha = c.Contributions.FirstOrDefault(cn => cn.SimchaId == simchaId).Amount;
                    }
                    else
                    {
                        c.AmountContributedToSimcha = 0;
                    }
                }
                return contributors;
            }
        }

        public void UpdateContributions(int simchaId, List<SimchaContribution> contributions)
        {
            using (var context = new SimchaContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Contributions WHERE SimchaId = @simchaId", new SqlParameter("@simchaId", simchaId));
                foreach (SimchaContribution sc in contributions)
                {
                    if(sc.Include == true)
                    {
                        context.Database.ExecuteSqlCommand("INSERT INTO Contributions (SimchaId, ContributorId, Amount, Date) " +
                                                        "VALUES (@simchaId, @contributorId, @amount, @date)",
                        new SqlParameter("@simchaId", simchaId),
                        new SqlParameter("@contributorId", sc.ContributorId),
                        new SqlParameter("@amount", sc.Amount),
                        new SqlParameter("@date", System.DateTime.Now));
                    }
                }
                context.SaveChanges();
            }
        }



    }
}
