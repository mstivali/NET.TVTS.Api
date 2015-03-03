using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEN4020.TVTS.Domain.StyleDetails.Transmission
{
    public class Transmission
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EquipmentType { get; set; }
        public string AutomaticType { get; set; }
        public string TransmissionType { get; set; }
        public string NumberOfSpeeds { get; set; }
    }
}
