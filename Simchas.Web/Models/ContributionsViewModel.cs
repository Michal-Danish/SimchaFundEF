using Simchas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simchas.Web.Models
{
    public class ContributionsViewModel
    {
        public IEnumerable<Contributor> Contributors { get; set; }
        public Simcha Simcha { get; set; }
    }
}
