using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class TransactionEditParameters
    {
        public int Id { get; set; }
        public int Shares { get; set; }
        public float UnitPrice { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int TickerId { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionTypeId { get; set; }
    }
}
