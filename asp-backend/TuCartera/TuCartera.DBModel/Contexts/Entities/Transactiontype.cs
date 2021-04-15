using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class Transactiontype
    {
        // Database properties
        public int id { get; set; }
        public string type { get; set; }
        public string? description { get; set; }

        // Entity relations
        public ICollection<Transaction> transactions { get; set; }
    }
}
