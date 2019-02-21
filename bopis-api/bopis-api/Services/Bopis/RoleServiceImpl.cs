using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;

namespace bopis_api.Services.Bopis
{
    public class RoleServiceImpl : IRoleService
    {
        private ModelContext modelContext = new ModelContext();

        public RoleServiceImpl()
        {

        }

        public List<Role> findByProfileIdAndStatusEqualToOne(long profileId)
        {
            List<Role> roles = (from p in modelContext.Profile
                                join pr in modelContext.ProfileRole on p.Id equals pr.ProfileId
                                join r in modelContext.Role on pr.RoleId equals r.Id
                                where pr.ProfileId == profileId && r.Status == true
                                select r).ToList();

            return roles;
        }
    }
}
