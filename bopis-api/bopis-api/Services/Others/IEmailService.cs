using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Others
{
    interface IEmailService
    {
        void sendRecoveryPassword(User user);
    }
}
