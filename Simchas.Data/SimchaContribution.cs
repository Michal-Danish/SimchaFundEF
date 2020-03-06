using System;
using System.Collections.Generic;
using System.Text;

namespace Simchas.Data
{
    public class SimchaContribution
    {
        public bool Include { get; set; }
        public decimal Amount { get; set; }
        public int ContributorId { get; set; }
    }
}
