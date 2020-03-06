using System;
using System.Collections.Generic;
using System.Text;

namespace Simchas.Data
{
    public class HistoryItem
    {
        public string ActionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
