using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
  public class ControlState
  {
    public string? Key { get; set; }

    public string? ControlType { get; set; }

    public string? Category { get; set; }

    public double OrdinalPosition { get; set; }

    public string? JsonState { get; set; }

    public bool HasValue => JsonState != null && ControlType != null;
  }
}
