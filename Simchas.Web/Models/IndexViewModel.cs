using Simchas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simchas.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Simcha> Simchas { get; set; }
        public int Contributors { get; set; }
    }
}
