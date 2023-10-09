using SolutionHelper.Framework.FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionHelper.Framework.VisualStudioObjects
{
  public  class PackageReference
  {
    public PackageReference()
    {
      
    }


    public PackageReference(PackageReferenceData data)
    {
      Name = data.Name;
      Version = data.Version;
      TargetFramework = data.TargetFramework;
    }

    public string Name { get; set; }
    public string Version { get; set; }
    public string TargetFramework { get; set; }

    public override string ToString()
    {
      return $"{Name} {Version}";
    }
  }
}
