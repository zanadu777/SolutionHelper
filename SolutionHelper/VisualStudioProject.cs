using SolutionHelper.Framework.VisualStudioObjects;
using SolutionHelper.Net.VisualStudioObjects;

namespace SolutionHelper
{
  public abstract class VisualStudioProject
  {

    public static VisualStudioProject Create(FileInfo file)
    {
      

      var text = System.IO.File.ReadAllText(file.FullName);
      VisualStudioProject project = null;
      if (text.StartsWith( """
                      <?xml version="1.0" encoding="utf-8"?>
                      """))
        project = new NetFrameworkProject(file,text);

      if (text.StartsWith("""
                          <Project Sdk="Microsoft.NET.Sdk">
                          """))
        project = new NetProject(text);  

     
      if (project == null)
        throw new Exception($"Project {file.FullName} has an unrecognized format");

      project.File= file;

      return project;
    }

    public string Name { get; set; }
    public FileInfo File { get; set; }
  }
}
