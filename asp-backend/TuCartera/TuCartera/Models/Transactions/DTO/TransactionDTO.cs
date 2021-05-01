using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int Shares { get; set; }
        public float UnitPrice { get; set; }
        public float ExchangeToUSD { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int TickerId { get; set; }
        public string TickerCode { get; set; }
        public string TickerName { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionTypeType { get; set; }
    }
}
