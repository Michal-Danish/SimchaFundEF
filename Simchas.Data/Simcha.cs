using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simchas.Data
{
    public class Simcha
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Contribution> Contributions { get; set; }

        [NotMapped]
        public int TotalContributors { get; set; }
        [NotMapped]
        public decimal TotalAmount { get; set; }
     }
}
