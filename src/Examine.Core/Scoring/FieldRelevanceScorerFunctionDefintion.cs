
namespace Examine.Scoring
{
    /// <summary>
    /// Boosts a field
    /// </summary>
    public class FieldRelevanceScorerFunctionDefintion : RelevanceScorerFunctionBaseDefintion
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="boost">Boost</param>
        public FieldRelevanceScorerFunctionDefintion(string fieldName, float boost) : base("Examine.Field", fieldName, boost)
        {
        }
    }
}
