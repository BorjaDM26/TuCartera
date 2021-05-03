using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class TickerProfileResponse
    {
        public string Symbol { get; set; }
        public float Price { get; set; }
        public string CompanyName { get; set; }
    }
}
