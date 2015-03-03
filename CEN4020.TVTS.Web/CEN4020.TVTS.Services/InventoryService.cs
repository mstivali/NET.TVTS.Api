using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEN4020.TVTS.Infrastructure;

namespace CEN4020.TVTS.Services
{
    public class InventoryService
    {
        public void AddVehicleToInventory()
        {
            using (var context = new TvtsDataEntities())
            {
                var vehicle = new vehicle()
                {
                    Id = Guid.NewGuid(),
                    ModelName = "Yaris",
                    StyleId = "2",
                    StyleTrim = "Basic",
                    Color = "Blue"
                };

                context.vehicles.Add(vehicle);

                context.SaveChangesAsync();
            }
        }

    }
}
