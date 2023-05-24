namespace Examine
{
    /// <summary>
    /// Defines a parameter for a Relevance Scorer
    /// </summary>
    public class RelevanceScorerParameterDefinition
    {
        public RelevanceScorerParameterDefinition(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }
    }
}
