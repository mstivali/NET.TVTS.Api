using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEN4020.TVTS.Domain.Maintenance
{
    public class MaintenanceItem
    {
        public int Id { get; set; }
        public string EngineCode { get; set; }
        public string TransmissionCode { get; set; }
        public int IntervalMileage { get; set; }
        public int IntervalMonth { get; set; }
        public int Frequency { get; set; }
        public string Action { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public float LaborUnits { get; set; }
        public float PartUnits { get; set; }
        public string DriveType { get; set; }
        public string ModelYear { get; set; }

    }
}
