using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CEN4020.TVTS.Api.Models;
using CEN4020.TVTS.Services;


namespace CEN4020.TVTS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return RedirectToAction("Models");
        }

        public async Task<ActionResult> Models()
        {
            //var test = new InventoryService();

            //test.AddVehicleToInventory();

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

            return View("Models", carModelsModel);
        }

        public async Task<ActionResult> Styles(string name, string id)
        {
            var edmundsService = new EdmundsService();
            var toyotaSedanModels = await edmundsService.GetEdmundsModelsData();

            var styles = new StylesModel()
            {
                ModelName = name,
                Styles = edmundsService.GetStylesWithModelId(toyotaSedanModels, id)
            };

            return View("Styles", styles);
        }

        public async Task<ActionResult> StyleDetails(string styleId)
        {
            var edmundsService = new EdmundsService();

            var styleDetailModel = new StyleDetailsModel
            {
                EngineDetails = await edmundsService.GetAvailableEnginesForStyleWithStyleId(styleId)
            };

            return View("StyleDetails", styleDetailModel);
        }

    }
}
