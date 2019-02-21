using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Others
{
    interface IJwtTokenService
    {
        string generate(string email);

        bool validate(string token);
    }
}
