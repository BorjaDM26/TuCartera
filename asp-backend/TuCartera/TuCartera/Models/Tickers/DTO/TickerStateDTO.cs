using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class TickerStateDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrentShares { get; set; }
        public float TotalBenefit { get; set; }
    }
}