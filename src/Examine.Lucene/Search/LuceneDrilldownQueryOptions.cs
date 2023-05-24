using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examine.Search;

namespace Examine.Lucene.Search
{
    public class LuceneDrilldownQueryOptions : IDrillDownQueryOptions
    {
        public IDrillDownQueryOptions Add(string dim, params string[] path) => throw new NotImplementedException();
        public IDrillDownQueryOptions Add(string dim, Func<INestedQuery, INestedBooleanOperation> inner) => throw new NotImplementedException();
        public IDrillDownQueryOptions AddDrillSidewaysDimensions(string dim) => throw new NotImplementedException();
    }
}
