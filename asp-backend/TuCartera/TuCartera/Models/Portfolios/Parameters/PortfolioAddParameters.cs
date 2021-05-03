using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class PortfolioAddParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> TickerIds { get; set; }
    }
}
