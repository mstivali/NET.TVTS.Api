using System.Collections.Generic;

namespace CEN4020.TVTS.Domain.CarModels
{
    public class Years
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IList<Styles> Styles { get; set; } 
    }
}
