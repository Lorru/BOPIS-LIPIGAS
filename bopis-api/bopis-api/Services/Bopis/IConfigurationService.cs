using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface IConfigurationService
    {
        List<Configuration> findByKeyAndStatusEqualToOne(string key);

        Configuration updateValueByIdAndStatusEqualToOne(Configuration configuration);

        Configuration findByIdAndStatusEqualToOne(long id);

        List<Configuration> findAll(int sort, string column);

        List<Configuration> findAllPaged(int page, int sort, string column);

        Configuration updateValueAndReadOnlyByIdAndStatusEqualToOne(Configuration configuration);
    }
}
