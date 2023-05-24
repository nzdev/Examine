using System.Collections.Generic;

namespace Examine.Scoring
{
    /// <summary>
    /// Boosts relevance based on a JavaScript function
    /// </summary>
    public class JavaScriptFunctionScorerFunctionDefinition : RelevanceScorerFunctionBaseDefintion
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="boost">Boost</param>
        /// <param name="javaScriptFunction">JavaScript Function</param>
        /// <param name="parameterMap">Map from Relevance parameter name to JavaScript parameter name</param>
        public JavaScriptFunctionScorerFunctionDefinition(string fieldName, float boost, string javaScriptFunction, IReadOnlyDictionary<string,string> parameterMap) : base("Examine.JavaScript", fieldName, boost)
        {
            JavaScriptFunction = javaScriptFunction;
            ParameterMap = parameterMap;
        }

        /// <summary>
        /// JavaScript Function
        /// </summary>
        public string JavaScriptFunction { get; }

        /// <summary>
        /// Map Relevance Parameter name to JavaScript function parameter name
        /// </summary>
        public IReadOnlyDictionary<string, string> ParameterMap { get; }
    }
}
