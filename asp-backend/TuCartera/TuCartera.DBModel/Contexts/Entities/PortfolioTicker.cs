using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class PortfolioTicker
    {
        // Entity relations
        public int ticker_id { get; set; }
        public Ticker ticker { get; set; }

        public int portfolio_id { get; set; }
        public Portfolio portfolio { get; set; }
    }
}
