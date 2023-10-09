using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionHelper.Framework.FileData
{
    public class ProjectReferenceData
    {
        private const string projectRelativePath = "ProjectRelativePath";
        private const string projectGuid = "ProjectGuid";
        private const string projectName = "ProjectName";
        static ProjectReferenceData()
        {
            var rxText = $$"""
                     <ProjectReference\s+Include\s*=\s*"(?<{{projectRelativePath}}>.*?)"\s*>\s*
                     <Project>(?<{{projectGuid}}>{.*?})\s*</Project>\s*
                     <Name>(?<{{projectName}}>.*?)</Name>\s*
                     </ProjectReference>
                     """;
            rx = new Regex(rxText, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);
        }


        public static List<ProjectReferenceData> Parse(string text)
        {
            var references = new List<ProjectReferenceData>();
            var matches = rx.Matches(text);
            foreach (Match match in matches)
                references.Add(new ProjectReferenceData(match));

            return references;
        }
        public ProjectReferenceData(Match match)
        {
            Name = match.Groups[projectName].Value;
            ProjectRelativePath = match.Groups[projectRelativePath].Value;
            ProjectGuid = match.Groups[projectGuid].Value;
        }

        private static readonly Regex rx;
        public string ProjectRelativePath { get; set; }
        public string ProjectGuid { get; set; }
        public string Name { get; set; }

    }
}
