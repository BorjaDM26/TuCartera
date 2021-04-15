using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class User
    {
        // Database properties
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public Byte[] pass { get; set; }

        // Entity relations
        public ICollection<Transaction> transactions { get; set; }
        public ICollection<Portfolio> portfolios { get; set; }
    }
}
