using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class Portfolio
    {
        // Database properties
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }

        // Entity relations
        public int user_id { get; set; }
        public User user { get; set; }
        public ICollection<Ticker> tickers { get; set; }

    }
}
