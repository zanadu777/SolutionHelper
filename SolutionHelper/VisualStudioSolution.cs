using System.Diagnostics.CodeAnalysis;
using System.Text;
using SolutionHelper.Framework.FileData;
using SolutionHelper.Framework.VisualStudioObjects;

namespace SolutionHelper
{
  public class VisualStudioSolution
  {
    public VisualStudioSolution()
    {}

    public List<VisualStudioProject> Projects { get; set; } = new();

    public VisualStudioSolution(FileInfo file)
    {
      SolutionFile = file;
      var text = File.ReadAllText(file.FullName);

      Name= Path.GetFileNameWithoutExtension(file.FullName);

      var solutionDirPath = Path.GetDirectoryName(file.FullName);

      var projects = SolutionProjectData.Parse(text);
      foreach (var item in projects)
      {
        if (item.ProjectGuid != "{88E2D123-EDF0-498F-B83F-93CCD9E50CF1}") //not a solution folder
        {
          var projectFullPath = Path.Combine(solutionDirPath, item.RelativePath);
          var project = VisualStudioProject.Create(new FileInfo(projectFullPath));
          project.Name = item.Name;
          Projects.Add(project);
        }
      }

      NugetPackageDirectory = GetNugetPackageDirectory(file);
    }


    private DirectoryInfo GetNugetPackageDirectory(FileInfo file)
    {
      var defaultLocationForNugetConfig = Path.Combine(file.DirectoryName, "Nuget.config");

      if (File.Exists(defaultLocationForNugetConfig))
      {
        var nugetText = File.ReadAllText(defaultLocationForNugetConfig);
      }

      var location = defaultLocationForNugetConfig;
      return new DirectoryInfo(defaultLocationForNugetConfig);

    }
    public string Name { get; set; }

    public DirectoryInfo NugetPackageDirectory { get; set; }
    public FileInfo SolutionFile { get; set; }


    public string Details()
    {
      var l1 = "  ";
      var l2 = "    ";
      var l3 = "      ";
      var l4 = "        ";
      var sb = new StringBuilder();
      sb.AppendLine(Name);
      foreach (var project in Projects)
      {
        sb.AppendLine($"{l1}{project.Name}");
        if (project is NetFrameworkProject frameworkProject)
        {
          if (frameworkProject.ProjectReferences.Any())
          {
            sb.AppendLine($"{l2}Project References");
            foreach (var reference in frameworkProject.ProjectReferences)
            {
              sb.AppendLine($"{l3}{reference.Name}");
            }
          }

          if (frameworkProject.AssemblyReferences.Any())
          {
            sb.AppendLine($"{l2}Assembly References");
            foreach (var reference in frameworkProject.AssemblyReferences)
            {
              sb.AppendLine($"{l3}{reference.ToString()}");
            }
          }

          if (frameworkProject.PackageReference.Any())
          {
            sb.AppendLine($"{l2}Package References");
            foreach (var reference in frameworkProject.PackageReference)
            {
              sb.AppendLine($"{l3}{reference.ToString()}");
            }
          }
        }
      }


      return sb.ToString();
    }
  }
}