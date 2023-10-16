using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionHelperWpf.DesignTime
{
  public static class DesignTimeData
  {
    public static HelpersVm HelpersVm
    {
      get
      {
        var vm = new HelpersVm();
        vm.Controls = HelperControl.StandardHelperControls();
        return vm;
      }
    }

  }
}
