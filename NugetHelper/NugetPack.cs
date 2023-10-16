using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NugetHelper
{
  public class NugetPack
  {
    public void PackageFromProject(string projectPath, string outputDirectory)
    {
      var command = $"""
                    /K
                    nuget.exe pack "{projectPath}" -Build -Symbols -OutputDirectory "{outputDirectory}"
                    """;
      var startInfo = new ProcessStartInfo("cmd.exe")
      {
        Arguments = command,
        UseShellExecute = false
      };

      var process = Process.Start(startInfo);
      process?.WaitForExit();

      process?.Kill();
    }
  }
}
