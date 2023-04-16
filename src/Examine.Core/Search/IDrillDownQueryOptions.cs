using System;

namespace Examine.Search
{
    public interface IDrillDownQueryOptions
    {
        /// <summary>
        /// Adds one dimension of drill downs; if you pass the same
        /// dimension more than once it is OR'd with the previous
        /// cofnstraints on that dimension, and all dimensions are
        /// AND'd against each other and the base query. 
        /// </summary>
        IDrillDownQueryOptions Add(string dim, params string[] path);

        /// <summary>
        /// Expert: add a custom drill-down subQuery.  Use this
        /// when you have a separate way to drill-down on the
        /// dimension than the indexed facet ordinals. 
        /// </summary>
        IDrillDownQueryOptions Add(string dim, Func<INestedQuery, INestedBooleanOperation> inner);

        /// <summary>
        /// Expert: add a custom drill-down Filter, e.g. when
        /// drilling down after range faceting. 
        /// </summary>
        //public void Add(string dim, Filter subFilter)

        IDrillDownQueryOptions AddDrillSidewaysDimensions(string dim);
    }
}
