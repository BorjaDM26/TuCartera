using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class Transaction
    {
        // Database properties
        public int id { get; set; }
        public int number_of_shares{ get; set; }
        public float unit_price { get; set; }
        public DateTime date { get; set; }
        public string? comment { get; set; }

        // Entity relations
        public int user_id { get; set; }
        public User user { get; set; }
        public int transaction_type_id { get; set; }
        public Transactiontype transaction_type { get; set; }
        public int currency_id { get; set; }
        public Currency currency { get; set; }
        public int ticker_id { get; set; }
        public Ticker ticker { get; set; }

    }
}
