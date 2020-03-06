using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Simchas.Data;
using Simchas.Web.Models;

namespace Simchas.Web.Controllers
{
    public class SimchasController : Controller
    {
        private string _connectionString;
        public SimchasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new SimchaRepository(_connectionString);
            IndexViewModel vm = new IndexViewModel();
            vm.Simchas = repo.GetSimchas();
            vm.Contributors = repo.GetContributorCount();
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddSimcha(Simcha s)
        {
            var repo = new SimchaRepository(_connectionString);
            repo.AddSimcha(s);
            return RedirectToAction("Index");
        }

        public IActionResult SimchaContributions(int id)
        {
            var repo = new SimchaRepository(_connectionString);
            IEnumerable<Contributor> contributors = repo.GetSimchaContributors(id);
            Simcha simcha = repo.GetSimcha(id);
            ContributionsViewModel vm = new ContributionsViewModel();
            vm.Contributors = contributors;
            vm.Simcha = simcha;
            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateContributions(List<SimchaContribution> contributors, int simchaId)
        {
            var repo = new SimchaRepository(_connectionString);
            repo.UpdateContributions(simchaId, contributors);
            return RedirectToAction("Index");
        }



    }
}
