using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCartera.DBModel.Contexts.Entities
{
    public class SpTransactionTypeItemResult
    {
        public int transaction_type_id { get; set; }
        public string transaction_type_type { get; set; }
        public string transaction_type_description { get; set; }
    }
}
