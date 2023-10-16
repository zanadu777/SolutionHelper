using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolutionHelper.Framework.FileData;

namespace SolutionHelper.Framework.VisualStudioObjects
{
  public class NetFrameworkProject : VisualStudioProject
  {

    public List<AssemblyReference> AssemblyReferences { get; set; } = new();
    public List<ProjectReference> ProjectReferences { get; set; } = new();

    public List<PackageReference> PackageReference { get; set; } = new();

    public NetFrameworkProject(FileInfo file, string text)
    {
      var referenceData = AssemblyReferenceData.Parse(text);
      foreach (var item in referenceData)
        AssemblyReferences.Add(new AssemblyReference(item));

      var projectReferenceData = ProjectReferenceData.Parse(text);
      foreach (var item in projectReferenceData)
        ProjectReferences.Add(new ProjectReference(item));

      if (text.Contains("packages.config"))
      {
        var packagesConfigPath = Path.Combine(file.DirectoryName, "packages.config");
        var packageText = System.IO.File.ReadAllText(packagesConfigPath);
        var packegeReferenceData = PackageReferenceData.Parse(packageText);
        foreach (var item in packegeReferenceData)
          PackageReference.Add(new PackageReference(item));
      }

      var versionExtractor = new ProjectFrameworkVersion();
      Version = versionExtractor.Extract(text);
    }
  }
}
