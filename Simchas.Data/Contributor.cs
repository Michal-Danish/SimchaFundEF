using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Simchas.Data
{
    public class Contributor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }
        public List<Deposit> Deposits { get; set; }
        public List<Contribution> Contributions { get; set; }

        [NotMapped]
        public decimal Balance { get; set; }
        [NotMapped]
        public decimal AmountContributedToSimcha { get; set; }
    }
}
