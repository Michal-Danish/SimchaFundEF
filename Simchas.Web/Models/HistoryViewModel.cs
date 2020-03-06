using Simchas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simchas.Web.Models
{
    public class HistoryViewModel
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<HistoryItem> Actions { get; set; }
    }
}
