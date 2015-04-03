using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CEN4020.TVTS.Api.Models;
using CEN4020.TVTS.Infrastructure;
using Newtonsoft.Json;

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

        public dynamic GetInventory()
        {
            dynamic response;

            using (var context = new TvtsDataEntities())
            {
                var responseEntity = context.vehicles.ToList();

                response = responseEntity;
            }

            return response;
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            var industryDetailString = JsonConvert.SerializeObject(obj);
            var toBytes = Encoding.ASCII.GetBytes(industryDetailString);
            return toBytes;
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            var industryDetailString = Encoding.ASCII.GetString(arrBytes);
            var industryDetailResponse = JsonConvert.DeserializeObject(industryDetailString);
            return industryDetailResponse;
        }

    }
}
