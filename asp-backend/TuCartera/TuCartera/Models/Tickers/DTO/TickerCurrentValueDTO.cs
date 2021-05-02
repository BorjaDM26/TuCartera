using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class TickerCurrentValueDTO
    {
        public int Id { get; set; }
        public float CurrentValue { get; set; }

        public TickerCurrentValueDTO(int id, float value)
        {
            this.Id = id;
            this.CurrentValue = value;
        }
    }
}
