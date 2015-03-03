using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEN4020.TVTS.Api.Models;
using CEN4020.TVTS.Infrastructure;

namespace CEN4020.TVTS.Services
{
    public class InventoryService
    {
        public void AddVehicleToInventory(InventoryOrder inventoryOrder)
        {
            using (var context = new TvtsDataEntities())
            {
                var vehicle = new vehicle()
                {
                    Id = Guid.NewGuid(),
                    ModelName = inventoryOrder.ModelName,
                    StyleId = inventoryOrder.StyleId,
                    StyleTrim = inventoryOrder.StyleTrim,
                    Color = inventoryOrder.Color
                };

                context.vehicles.Add(vehicle);

                context.SaveChangesAsync();
            }
        }

    }
}
