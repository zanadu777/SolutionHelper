using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GuiShared;
using NugetHelperControls;
using SolutionHelperControls;

namespace SolutionHelperWpf
{
  public class HelpersVm : INotifyPropertyChanged
  {
    private HelperControl selectedControl;
    public ObservableCollection<HelperControl> Controls { get; set; } = new ObservableCollection<HelperControl>();


    static HelpersVm()
    {
      controlGenerators[HelperType.SwapInOutNuget] = () => new SolutionNugetSwap();
      controlGenerators[HelperType.CreateNuget] = () => new CreateNuget();
      controlGenerators[HelperType.UpdateNuget] = () => new UpdateNuget();
    }


    public HelperControl SelectedControl
    {
      get => selectedControl;
      set
      {
        if (Equals(value, selectedControl)) return;
        selectedControl = value;
        if (!ControlInstances.ContainsKey(value.Id))
        {
          ControlInstances[value.Id] = controlGenerators[value.Type]();
          if (ControlInstances[value.Id] is IIsolatedStoragePersistent isolatedStoragePersistent)
            isolatedStoragePersistent.IsolatedStoragePrefix = $"{value.Type}-{value.Id}";
        }

        var index = Controls.IndexOf(value);
        IsolatedStorageHelper.WriteToIsolatedStorage( "MainWindow-SelectedControl", index.ToString());
        SelectedUserControl = ControlInstances[value.Id];
        OnPropertyChanged();
      }
    }


    public void Initialize()
    {
      Controls = HelperControl.StandardHelperControls();
      try
      {
        var index = int.Parse(IsolatedStorageHelper.ReadFromIsolatedStorage("MainWindow-SelectedControl"));
        SelectedControl= Controls[index];
      }
      catch
      {
        SelectedControl = Controls[0];
      }
    }

    private static readonly Dictionary<HelperType, Func<UserControl>> controlGenerators = new();

    Dictionary<int, UserControl> ControlInstances = new Dictionary<int, UserControl>();
    private UserControl selectedUserControl;

    public UserControl SelectedUserControl
    {
      get => selectedUserControl;
      set
      {
        if (Equals(value, selectedUserControl)) return;
        selectedUserControl = value;
        OnPropertyChanged();
      }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(field, value)) return false;
      field = value;
      OnPropertyChanged(propertyName);
      return true;
    }
  }
}
