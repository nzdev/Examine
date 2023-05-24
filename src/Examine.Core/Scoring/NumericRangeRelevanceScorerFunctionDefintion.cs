namespace Examine.Scoring
{
    /// <summary>
    /// Boosts a range of numeric values
    /// </summary>
    public class NumericRangeRelevanceScorerFunctionDefintion : RelevanceScorerFunctionBaseDefintion
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="boost">Boost</param>
        public NumericRangeRelevanceScorerFunctionDefintion(string fieldName, float boost, int boostRangeStart, int boostRangeEnd) : base("Examine.NumericRange", fieldName, boost)
        {
            BoostRangeStart = boostRangeStart;
            BoostRangeEnd = boostRangeEnd;
        }

        /// <summary>
        /// Lower bound of the range of values to boost
        /// </summary>
        public int BoostRangeStart { get; }

        /// <summary>
        /// Upper bound of the range of values to boost
        /// </summary>
        public int BoostRangeEnd { get; }
    }
}
