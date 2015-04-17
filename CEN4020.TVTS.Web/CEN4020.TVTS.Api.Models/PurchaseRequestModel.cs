using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEN4020.TVTS.Api.Models
{
    public class PurchaseRequestModel
    {
        public Guid CustomerId { get; set; }
        public Guid InventoryId { get; set; }
    }
}

