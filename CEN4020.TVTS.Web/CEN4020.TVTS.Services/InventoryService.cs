using System;
using System.Linq;
using System.Text;
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
                    ModelIdName = inventoryOrder.ModelIdName,
                    ModelName = inventoryOrder.ModelName,
                    StyleId = inventoryOrder.StyleId,
                    StyleTrim = inventoryOrder.StyleTrim,
                    Color = inventoryOrder.Color,
                    Purchased = 0,
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
                var responseEntity = context.vehicles.Where(x => x.Purchased != 1).ToList();

                response = responseEntity;
            }

            return response;
        }

        public bool MarkInventoryPurchased(Guid customerId, Guid inventoryId)
        {
            using (var context = new TvtsDataEntities())
            {
                var responseEntity = context.vehicles.SingleOrDefault(x => x.Id.Equals(inventoryId));

                if (responseEntity == null) return false;

                responseEntity.CustomerId = customerId;
                responseEntity.DatePurchased = DateTime.Now;
                responseEntity.Purchased = 1;

                context.SaveChangesAsync();

                return true;
            }           
        }

        public bool DeleteVehicle(Guid inventoryId)
        {
            using (var context = new TvtsDataEntities())
            {
                var vehicleEntity = context.vehicles.SingleOrDefault(x => x.Id.Equals(inventoryId));
                if (vehicleEntity == null) return false;
                context.vehicles.Remove(vehicleEntity);
                context.SaveChangesAsync();
            }

            return true;
        }

        public dynamic GetCarsSold()
        {
            dynamic response;

            using (var context = new TvtsDataEntities())
            {
                var responseEntity = context.vehicles.Where(x => x.Purchased == 1).ToList();

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
