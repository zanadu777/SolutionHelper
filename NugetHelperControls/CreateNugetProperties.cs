using System;
using Helper.Shared;

namespace NugetHelperControls
{
  public class CreateNugetProperties:HelperControlProperties
  {
    public string? PathToProject { get; set; }
    public string? OutputDirectory { get; set; }
  }
}
