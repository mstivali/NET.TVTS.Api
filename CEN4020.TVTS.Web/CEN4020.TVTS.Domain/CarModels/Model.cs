using System.Collections.Generic;

namespace CEN4020.TVTS.Domain.CarModels
{
    public class Model
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public IList<Years> Years { get; set; } 
    }
}
