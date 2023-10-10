using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionHelper.Framework.FileData
{
  public class SolutionProjectData
  {
    public string Name { get; set; }
    public string RelativePath { get; set; }
    public string ProjectGuid { get; set; }

    private const string projectName = "ProjectName";
    private const string projectPath = "ProjectPath";
    private const string projectGuid = "ProjectGuid";

    private static readonly Regex rx;
    static SolutionProjectData()
    {
      var rxText = """
                     Project\("(?<ProjectType>{.*?})"\)\s*=\s*"(?<ProjectName>.*?)"\s*,\s*"(?<ProjectPath>.*?)"\s*,\s*"(?<ProjectGuid>.*?)"
                     """;
      rx = new Regex(rxText, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);
    }

    public SolutionProjectData(Match match)
    {
      Name = match.Groups[projectName].Value;
      RelativePath = match.Groups[projectPath].Value;
      ProjectGuid = match.Groups[projectGuid].Value;
    }

    public static List<SolutionProjectData> Parse(string text)
    {
      var references = new List<SolutionProjectData>();
      var matches = rx.Matches(text);
      foreach (Match match in matches)
        references.Add(new SolutionProjectData(match));

      return references;
    }
  }
}
