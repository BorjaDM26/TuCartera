using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.Models
{
    public class TransactionEditParameters: TransactionAddParameters
    {
        public int Id { get; set; }
    }
}
