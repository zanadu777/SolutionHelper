using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionHelper.Framework.FileData
{
  public class PackageReferenceData
  {
    public string Name { get; set; }
    public string Version { get; set; }
    public string TargetFramework { get; set; }

    private const string packageName = "PackageName";
    private const string version = "Version";
    private const string targetFramework = "TargetFramework";

    private static readonly Regex rx;
    static PackageReferenceData()
    {
      var rxText = $"""
                     <package\s+id="(?<{packageName}>.*?)"\s+version\s*=\s*"(?<{version}>.*?)"\s+targetFramework\s*=\s*"(?<{targetFramework}>.*?)"\s*/>
                     """;
      rx = new Regex(rxText, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);
    }

    public PackageReferenceData(Match match)
    {
      Name = match.Groups[packageName].Value;
      Version = match.Groups[version].Value;
      TargetFramework = match.Groups[targetFramework].Value;
    }

    public static List<PackageReferenceData> Parse(string text)
    {
      var references = new List<PackageReferenceData>();
      var matches = rx.Matches(text);
      foreach (Match match in matches)
        references.Add(new PackageReferenceData(match));

      return references;
    }
  }
}
