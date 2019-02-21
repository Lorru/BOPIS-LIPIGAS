using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    public class ProfileServiceImpl : IProfileService
    {
        private ModelContext modelContext = new ModelContext();

        public ProfileServiceImpl()
        {

        }

        public List<Profile> findByAllStatusEqualToOne()
        {
            List<Profile> profiles = (from p in modelContext.Profile
                                      where p.Status == true
                                      select p).ToList();

            return profiles;
        }
    }
}
