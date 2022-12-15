using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
         Task<DataSet> Query(FormattableString query);
    }
}
