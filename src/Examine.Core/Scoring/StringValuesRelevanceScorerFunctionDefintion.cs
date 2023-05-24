
namespace Examine.Scoring
{
    /// <summary>
    /// Boosts based on strings matching values in field
    /// </summary>
    public class StringValuesRelevanceScorerFunctionDefintion : RelevanceScorerFunctionBaseDefintion
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="boost">Boost</param>
        public StringValuesRelevanceScorerFunctionDefintion(string fieldName, float boost, string stringsRelevanceParameterName) : base("Examine.NumericRange", fieldName, boost)
        {
            StringsRelevanceParameterName = stringsRelevanceParameterName;
        }

        /// <summary>
        /// The name of the relevance parameter that contains a comma seperated list of strings to match on
        /// </summary>
        public string StringsRelevanceParameterName { get; }
    }
}
