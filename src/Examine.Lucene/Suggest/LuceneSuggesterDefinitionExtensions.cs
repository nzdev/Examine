using Examine.Lucene.Suggest.Directories;
using Lucene.Net.Analysis;

namespace Examine.Lucene.Suggest
{
    /// <summary>
    /// Extension methods to help configure Lucene.NET suggesters
    /// </summary>
    public static class LuceneSuggesterDefinitionExtensions
    {
        public static SuggesterDefinitionCollection AddAnalyzingInfixSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new AnalyzingInfixSuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddAnalyzingInfixSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields, Analyzer? queryTimeAnalyzer)
        {
            suggesterDefinitions.AddOrUpdate(new AnalyzingInfixSuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory(), queryTimeAnalyzer));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddAnalyzingSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new AnalyzingSuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddAnalyzingSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields, Analyzer? queryTimeAnalyzer)
        {
            suggesterDefinitions.AddOrUpdate(new AnalyzingSuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory(), queryTimeAnalyzer));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddFuzzySuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new FuzzySuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddFuzzySuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields, Analyzer? queryTimeAnalyzer)
        {
            suggesterDefinitions.AddOrUpdate(new FuzzySuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory(), queryTimeAnalyzer));
            return suggesterDefinitions;
        }


        public static SuggesterDefinitionCollection AddSpellCheckerSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new DirectSpellCheckerDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddLevensteinDistanceSpellCheckerSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new LevensteinDistanceSuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }

        public static SuggesterDefinitionCollection AddJaroWinklerDistanceSpellCheckerSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new JaroWinklerDistanceDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }
        public static SuggesterDefinitionCollection AddNGramDistanceSpellCheckerSuggester(this SuggesterDefinitionCollection suggesterDefinitions, string suggesterName, string[] sourceFields)
        {
            suggesterDefinitions.AddOrUpdate(new NGramDistanceSuggesterDefinition(suggesterName, sourceFields, new RAMSuggesterDirectoryFactory()));
            return suggesterDefinitions;
        }
    }
}
