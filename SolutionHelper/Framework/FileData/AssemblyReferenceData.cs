using System.Text.RegularExpressions;

namespace SolutionHelper.Framework.FileData
{
    public class AssemblyReferenceData
    {
        private const string referenceName = "referenceName";
        private const string version = "version";
        private const string culture = "culture";
        private const string publicKeyToken = "publicKeyToken";
        private const string processorArchitecture = "processorArchitecture";
        private const string hintPath = "hintPath";
        static AssemblyReferenceData()
        {
            var rxText = $"""
                   <Reference\s+Include="(?<{referenceName}>.*?)\s*,\s*
                   Version\s*=\s*(?<{version}>[0-9\.]+)\s*,\s*
                   Culture\s*=(?<{culture}>.*?)\s*,\s
                   PublicKeyToken\s*=(?<{publicKeyToken}>.*?)\s*,\s*
                   processorArchitecture\s*=\s*(?<{processorArchitecture}>.*?)\s*"\s*>\s*
                   <HintPath>(?<{hintPath}>.*?)</HintPath>
                   .*?</Reference>
                   """;
            rx = new Regex(rxText, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);
        }

        private static readonly Regex rx;
        public static List<AssemblyReferenceData> Parse(string text)
        {
            var references = new List<AssemblyReferenceData>();
            var matches = rx.Matches(text);
            foreach (Match match in matches)
                references.Add(new AssemblyReferenceData(match));

            return references;
        }
        public AssemblyReferenceData(Match match)
        {
            Name = match.Groups[referenceName].Value;
            Version = match.Groups[version].Value;
            Culture = match.Groups[culture].Value;
            PublicKeyToken = match.Groups[publicKeyToken].Value;
            HintPath = match.Groups[hintPath].Value;
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public string Culture { get; set; }
        public string PublicKeyToken { get; set; }
        public string HintPath { get; set; }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }
    }
}
