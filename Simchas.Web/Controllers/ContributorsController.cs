using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Simchas.Data;
using Simchas.Web.Models;

namespace Simchas.Web.Controllers
{
    public class ContributorsController : Controller
    {
        private string _connectionString;
        public ContributorsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new SimchaRepository(_connectionString);
            IEnumerable<Contributor> contributors = repo.GetContributors();
            return View(contributors);
        }

        [HttpPost]
        public IActionResult AddContributor(Contributor c, Deposit d)
        {
            var repo = new SimchaRepository(_connectionString);
            repo.AddContributor(c, d);
            return Redirect("/contributors/index");
        }

        [HttpPost]
        public IActionResult EditContributor(Contributor c)
        {
            var repo = new SimchaRepository(_connectionString);
            repo.EditContributor(c);
            return Redirect("/contributos/index");
        }

        [HttpPost]
        public IActionResult AddDeposit(Deposit d)
        {
            var repo = new SimchaRepository(_connectionString);
            repo.AddDeposit(d);
            return Redirect("/contributors/index");
        }


        public IActionResult ShowHistory(int id)
        {
            var repo = new SimchaRepository(_connectionString);
            var vm = new HistoryViewModel();
            Contributor contributor = repo.GetContributor(id);
            List<HistoryItem> actions = new List<HistoryItem>();
            foreach(Deposit d in contributor.Deposits)
            {
                actions.Add(new HistoryItem
                {
                    ActionType = "Deposit",
                    Date = d.Date,
                    Amount = d.Amount
                });
            }
            foreach(Contribution c in contributor.Contributions)
            {
                actions.Add(new HistoryItem
                {
                    ActionType = $"Contribution for {c.Simcha.Name}",
                    Date = c.Date,
                    Amount = c.Amount
                });
            }
            vm.Name = contributor.FirstName + " " + contributor.LastName;
            vm.Balance = contributor.Deposits.Sum(d => d.Amount) - contributor.Contributions.Sum(c => c.Amount);
            vm.Actions = actions;
            return View(vm);
        }
    }
}