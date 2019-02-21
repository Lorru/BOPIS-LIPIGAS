using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    public class CylinderServiceImpl : ICylinderService
    {
        private ModelContext modelContext = new ModelContext();

        public CylinderServiceImpl()
        {

        }

        public List<Cylinder> findAllAndStatusEqualToOne()
        {
            List<Cylinder> cylinders = (from c in modelContext.Cylinder
                                        where c.Status == true
                                        select c).ToList();

            return cylinders;
        }
    }
}
