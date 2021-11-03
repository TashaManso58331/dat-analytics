using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dat.Access.Loads
{
    public interface ISearchService
    {
        Task<List<Load>> Search(object filter);
    }
}
