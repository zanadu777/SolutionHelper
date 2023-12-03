using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
  public class ApplicationState
  {
    public string Key { get; set; }

    public string SelectedKey { get; set; }

    public Dictionary<string, ControlState> ControlStates { get; set; }= new();
  }
}
