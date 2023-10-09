using System.Text;
using System.Text.RegularExpressions;
using SolutionHelper.Framework.VisualStudioObjects;

namespace SolutionHelper
{
  public class VisualStudioSolution
  {
    public VisualStudioSolution()
    {

    }

    public List<VisualStudioProject> Projects { get; set; } = new();

    public VisualStudioSolution(FileInfo file)
    {
      var lines = File.ReadAllLines(file.FullName);
      var projectRxText = """
                     Project\("(?<ProjectType>{[A-Z0-9-]+})"\)\s*=\s*"(?<ProjectName>.*?)"\s*,\s*"(?<ProjectPath>.*?)"\s*,\s*"(?<ProjectGuid>.*?)"
                     """;
      var projectRx = new Regex(projectRxText);

      Name= Path.GetFileNameWithoutExtension(file.FullName);

      var solutionDirPath = Path.GetDirectoryName(file.FullName);
      foreach (var line in lines)
      {

        var match = projectRx.Match(line);
        if (match.Success)
        {
          var projectPath = match.Groups["ProjectPath"].Value;
          var projectFullPath = Path.Combine(solutionDirPath, projectPath);
          var projectName = match.Groups["ProjectName"].Value;
          var project = VisualStudioProject.Create(new FileInfo(projectFullPath));
          project.Name = projectName;
          Projects.Add(project);
        }
      }

    }

    public string Name { get; set; }

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