using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using CEN4020.TVTS.Api.Models;
using CEN4020.TVTS.Services;
using Newtonsoft.Json.Linq;

namespace CEN4020.TVTS.Web.Controllers
{
    public class RequestsController : ApiController
    {
        [HttpGet]
        [Route("api/test")]
        public Task<IHttpActionResult> IsApiOnline()
        {
            return Task.FromResult<IHttpActionResult>(Ok("TVTS API is online"));
        }

        [HttpGet]
        [Route("api/models")]
        public async Task<IHttpActionResult> GetModels()
        {
            var edmundsService = new EdmundsService();
            var toyotaSedanModels = await edmundsService.GetEdmundsModelsData();
            var carModelsModel = new CarModelsModel()
            {
                Models = toyotaSedanModels.Models
                .Select(x => new CarModel()
                {
                    Identification = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return Ok(carModelsModel);
        }

        [HttpGet]
        [Route("api/styles")]
        public async Task<IHttpActionResult> GetStylesWithModelId([FromUri] string modelId)
        {
            var edmundsService = new EdmundsService();
            var toyotaSedanModels = await edmundsService.GetEdmundsModelsData();

            var styles = new StylesModel()
            {
                ModelName = modelId,
                Styles = edmundsService.GetStylesWithModelId(toyotaSedanModels, modelId)
            };

            return Ok(styles);
        }

        [HttpGet]
        [Route("api/specs")]
        public async Task<IHttpActionResult> GetVehicleSpecsWithStyleId([FromUri] string styleId)
        {
            var edmundsService = new EdmundsService();

            var vehicleSpecs = new VehicleSpecsModel()
            {
                EngineDetail = await edmundsService.GetAvailableEnginesForStyleWithStyleId(styleId),
                TransmissionDetail = await edmundsService.GetAvailableTransmissionsForStyleId(styleId),
                EquipmentDetail = await edmundsService.GetEquipmentDetailForStyleId(styleId)
            };

            return Ok(vehicleSpecs);

        }

        [HttpPost]
        [Route("api/vehicle/save")]
        public IHttpActionResult SaveVehicleToInventory([FromBody] object inventoryOrder)
        {
            //var inventoryService = new InventoryService();

            //inventoryService.AddVehicleToInventory(inventoryOrder);

            return Ok(inventoryOrder);
        }

    }
}
