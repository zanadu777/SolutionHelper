using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionHelperWpf
{
  public class HelperControl
  {
    public string Title { get; set; }

    public HelperCategory Category { get; set; }

    public HelperType Type { get; set; }

    public double Ordinal { get; set; }

    public int Id { get; set; }


    public static ObservableCollection<HelperControl> StandardHelperControls()
    {
      var controls = new ObservableCollection<HelperControl>
      {
        new() { Title = "Swap In Nuget" , Type = HelperType.SwapInOutNuget, Category = HelperCategory.Solution,Ordinal=1, Id = 1},
        new() { Title = "Create Nuget Package" , Type = HelperType.CreateNuget, Category = HelperCategory.Nuget,Ordinal=2, Id = 2},
        new() { Title = "Update Nuget Package" , Type = HelperType.UpdateNuget, Category = HelperCategory.Nuget,Ordinal=3, Id = 3}
      };
      return controls;
    }
  }
}
