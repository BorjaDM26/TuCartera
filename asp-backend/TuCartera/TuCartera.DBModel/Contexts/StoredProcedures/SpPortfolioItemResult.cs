using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class SpPortfolioItemResult
    {
        public int portfolio_id { get; set; }
        public string portfolio_name { get; set; }
        public bool portfolio_global { get; set; }
        public string portfolio_description { get; set; }
        public Nullable<int> ticker_id { get; set; }
    }
}
