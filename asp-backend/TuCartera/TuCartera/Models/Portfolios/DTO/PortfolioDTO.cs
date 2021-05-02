using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class PortfolioDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGlobal { get; set; }
        public string Description { get; set; }
        public List<int> TickerIds { get; set; }
    }
}
