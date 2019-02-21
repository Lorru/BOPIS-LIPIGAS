using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Cylinder
    {
        public Cylinder()
        {
            CylinderByLocal = new HashSet<CylinderByLocal>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Kg { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }

        [JsonIgnore] public virtual ICollection<CylinderByLocal> CylinderByLocal { get; set; }
    }
}
