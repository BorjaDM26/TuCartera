using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class SpTransactionItemResult
    {
        public int transaction_id { get; set; }
        public decimal transaction_shares { get; set; }
        public decimal transaction_unit_price { get; set; }
        public DateTime transaction_date { get; set; }
        public string transaction_comment { get; set; }
        public int ticker_id { get; set; }
        public string ticker_code { get; set; }
        public string ticker_name { get; set; }
        public int currency_id { get; set; }
        public string currency_code { get; set; }
        public int transaction_type_id { get; set; }
        public string transaction_type_type { get; set; }
    }
}
