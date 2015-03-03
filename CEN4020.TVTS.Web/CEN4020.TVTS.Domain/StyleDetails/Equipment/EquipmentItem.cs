using System.Collections.Generic;

namespace CEN4020.TVTS.Domain.StyleDetails.Equipment
{
    public class EquipmentItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EquipmentType { get; set; }
        public string Availability { get; set; }
        public List<ItemAttribute> Attributes { get; set; } 
    }
}
