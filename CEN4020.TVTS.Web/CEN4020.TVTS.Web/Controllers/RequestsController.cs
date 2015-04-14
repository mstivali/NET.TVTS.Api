using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CEN4020.TVTS.Api.Models;
using CEN4020.TVTS.Infrastructure;
using CEN4020.TVTS.Services;
using System.Web.Http.Cors;

namespace CEN4020.TVTS.Web.Controllers
{
    [EnableCors (origins:"*",headers:"*", methods:"*")]
    public class RequestsController : ApiController
    {
        [HttpGet]
        [Route("api/test")]
        public Task<IHttpActionResult> IsApiOnline()
        {
            return Task.FromResult<IHttpActionResult>(Ok("TVTS API is online"));
        }

        [HttpGet]
        [Route("api/cars")]
        public OkNegotiatedContentResult<object> GetCarsJson()
        {
            var edmundsService = new EdmundsService();

            var carsJson = edmundsService.GetCarsJson();

            return Ok(carsJson);

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
        public IHttpActionResult SaveVehicleToInventory([FromBody] InventoryOrder inventoryOrder)
        {
            var inventoryService = new InventoryService();

            inventoryService.AddVehicleToInventory(inventoryOrder);

            return Ok();
        }

        [HttpGet]
        [Route("api/vehicles")]
        public IHttpActionResult GetVehicles()
        {
            var inventoryService = new InventoryService();

            var vehiclesList = inventoryService.GetInventory();

            return Ok(vehiclesList);
        }

        [HttpPost]
        [Route("api/customer/save")]
        public IHttpActionResult RegisterCustomer([FromBody] CustomerModel customer)
        {
            var customerService = new CustomerService();

            customerService.AddNewCustomer(customer);

            return Ok();
        }

        [HttpGet]
        [Route("api/customers")]
        public IHttpActionResult GetCustomers()
        {
            var customerService = new CustomerService();

            var customers = customerService.GetCustomersList();

            return Ok(customers);
        }

    }
}
