using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Simchas.Data
{
    public class Deposit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int ContributorId { get; set; }
        public Contributor Contributor { get; set; }

        [NotMapped]
        public string Action { get; set; }
    }
}
