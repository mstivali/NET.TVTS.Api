using System.Collections.Generic;
using CEN4020.TVTS.Domain.CarModels;

namespace CEN4020.TVTS.Api.Models
{
    public class StylesModel
    {
        public string ModelName { get; set; }
        public List<Styles> Styles { get; set; }
    }
}
