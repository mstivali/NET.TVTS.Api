using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CEN4020.TVTS.Domain.CarModels;
using CEN4020.TVTS.Domain.Maintenance;
using CEN4020.TVTS.Domain.StyleDetails.Engine;
using CEN4020.TVTS.Domain.StyleDetails.Equipment;
using CEN4020.TVTS.Domain.StyleDetails.Transmission;
using Newtonsoft.Json;

namespace CEN4020.TVTS.Services
{
    public class EdmundsService
    {
        private const string ApiKey = "27ggjjd3tthkmwh72tjgm52f";
        private const string Make = "toyota";
        private const string InventoryYear = "2015";
        private const string SubmodelType = "sedan";

        public object GetCarsJson()
        {
            var carsJsonPath = HttpContext.Current.Server.MapPath("~/Json/cars.json");

            var allText = File.ReadAllText(carsJsonPath);

            var jsonObject = JsonConvert.DeserializeObject(allText);

            return jsonObject;
        } 

        public async Task<EdmundsModelsList> GetEdmundsModelsData()
        {
            const string baseUri = "https://api.edmunds.com/api/vehicle/v2/" + Make + "/models";

            var builder = new UriBuilder(baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["state"] = "New";
            query["year"] = InventoryYear;
            //query["submodel"] = SubmodelType;
            query["view"] = "basic";
            query["fmt"] = "json";
            query["api_key"] = ApiKey;
            builder.Query = query.ToString();
            var uri = builder.ToString();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            var responseContent = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EdmundsModelsList>(responseContent);
        }

        public List<Styles> GetStylesWithModelId(EdmundsModelsList edmundsModelsList, string modelId)
        {
            var model = edmundsModelsList.Models.FirstOrDefault(x => x.Id == modelId);

            if (model == null) return null;

            var year = model.Years.FirstOrDefault(x => x.Year == Int32.Parse(InventoryYear));

            return year != null ? (List<Styles>)year.Styles : null;
        }

        public async Task<EngineDetail> GetAvailableEnginesForStyleWithStyleId(string styleId)
        {
            var baseUri = "https://api.edmunds.com/api/vehicle/v2/styles/" + styleId + "/engines";

            var builder = new UriBuilder(baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["availability"] = "standard";
            query["fmt"] = "json";
            query["api_key"] = ApiKey;
            builder.Query = query.ToString();
            var uri = builder.ToString();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            var responseContent = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<EngineDetail>(responseContent);
        }

        public async Task<TransmissionDetail> GetAvailableTransmissionsForStyleId(string styleId)
        {
            var baseUri = "https://api.edmunds.com/api/vehicle/v2/styles/" + styleId + "/transmissions";

            var builder = new UriBuilder(baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["availability"] = "standard";
            query["fmt"] = "json";
            query["api_key"] = ApiKey;
            builder.Query = query.ToString();
            var uri = builder.ToString();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            var responseContent = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransmissionDetail>(responseContent);
        }

        public async Task<EquipmentDetail> GetEquipmentDetailForStyleId(string styleId)
        {
            var baseUri = "https://api.edmunds.com/api/vehicle/v2/styles/" + styleId + "/equipment";

            var builder = new UriBuilder(baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["availability"] = "standard";
            query["equipmentType"] = "OTHER";
            query["fmt"] = "json";
            query["api_key"] = ApiKey;
            builder.Query = query.ToString();
            var uri = builder.ToString();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            var responseContent = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EquipmentDetail>(responseContent);
        }

        public async Task<MaintenanceReport> GetScheduledMaintanceData(string modelId)
        {
            var modelYearId = await GetModelYearIdWithModelId(modelId);

            const string baseUri = "https://api.edmunds.com/v1/api/maintenance/actionrepository/findbymodelyearid";

            var builder = new UriBuilder(baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["modelyearid"] = modelYearId;
            query["fmt"] = "json";
            query["api_key"] = ApiKey;
            builder.Query = query.ToString();
            var uri = builder.ToString();
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            var responseContent = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MaintenanceReport>(responseContent);
        }

        private async Task<string> GetModelYearIdWithModelId(string modelId)
        {
            var modelsData = await GetEdmundsModelsData();

            var selectedModel = modelsData.Models.SingleOrDefault(x => x.Id.Equals(modelId));

            if (selectedModel == null) return null;

            return selectedModel.Years.Select(x => x.Id).FirstOrDefault().ToString();
        }
    }
}
