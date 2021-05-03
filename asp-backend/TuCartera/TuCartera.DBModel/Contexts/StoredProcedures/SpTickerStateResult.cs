using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class SpTickerStateResult
    {
        public int ticker_id { get; set; }
        public string ticker_code { get; set; }
        public string ticker_name { get; set; }
        public decimal current_shares { get; set; }
        public decimal total_invested { get; set; }
    }
}
