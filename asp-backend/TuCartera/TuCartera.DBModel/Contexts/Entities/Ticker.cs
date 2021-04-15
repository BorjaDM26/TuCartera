using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class Ticker
    {
        // Database properties
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        // Entity relations
        public ICollection<Transaction> transactions { get; set; }
        public ICollection<PortfolioTicker> portfolios { get; set; }
    }
}
