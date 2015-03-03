using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEN4020.TVTS.Domain.StyleDetails.Engine;
using CEN4020.TVTS.Domain.StyleDetails.Equipment;
using CEN4020.TVTS.Domain.StyleDetails.Transmission;

namespace CEN4020.TVTS.Api.Models
{
    public class VehicleSpecsModel
    {
        public EngineDetail EngineDetail { get; set; }
        public TransmissionDetail TransmissionDetail { get; set; }
        public EquipmentDetail EquipmentDetail { get; set; }
    }
}
