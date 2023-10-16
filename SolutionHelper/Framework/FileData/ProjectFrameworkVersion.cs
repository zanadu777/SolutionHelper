using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionHelper.Framework.FileData
{
  public class ProjectFrameworkVersion
  {
    private const string VersionGroup = "VersionGroup";
 

    private static readonly Regex rx;

    static ProjectFrameworkVersion()
    {
      var rxText = $"""
                     <TargetFrameworkVersion>(?<{VersionGroup}>.*?)</TargetFrameworkVersion>
                    """;
      rx = new Regex(rxText, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);
    }

    public string Extract(string text)
    {
      var match = rx.Match(text);
      if (match.Success)
        return match.Groups[VersionGroup].Value;

      return string.Empty;
    }
  }
}
