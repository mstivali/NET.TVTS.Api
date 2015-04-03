using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEN4020.TVTS.Api.Models
{
    public class InventoryOrder
    {
        public string ModelIdName { get; set; }
        public string ModelName { get; set; }
        public string StyleTrim { get; set; }
        public string StyleId { get; set; }
        public string Color { get; set; }
        public string Options { get; set; }
    }
}
