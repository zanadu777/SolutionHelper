using SolutionHelper.Framework.FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SolutionHelper.Framework.VisualStudioObjects
{
  public class ProjectReference
  {
    public ProjectReference()
    {

    }

    public ProjectReference(ProjectReferenceData data)
    {
      Name = data.Name;
      Guid = data.ProjectGuid;
      RelativePath = data.ProjectRelativePath;
    }

    public string Name { get; set; }
    public string Guid { get; set; }
    public string RelativePath { get; set; }
  }
}
