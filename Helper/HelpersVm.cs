using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GuiShared;
using Helper;
using Helper.Shared;
using Newtonsoft.Json;
using SolutionHelperControls;
using Weikio.PluginFramework.Abstractions;
using Weikio.PluginFramework.Catalogs;

namespace SolutionHelperWpf
{
  public class HelpersVm : INotifyPropertyChanged
  {
    private IHelperControl selectedControl;
    public ObservableCollection<IHelperControl> Controls { get; set; } = new();

    public IHelperControl SelectedControl
    {
      get => selectedControl;
      set
      {
        if (Equals(value, selectedControl))
          return;

        selectedControl = value;
        SelectedUserControl = (UserControl)selectedControl;
        OnPropertyChanged();
      }
    }

    private string constantApplicationStatePath = "ApplicationState.json";
    public ApplicationState ApplicationState { get; set; } = new();

    Dictionary<string, Plugin> pluginMap = new();

    public void Initialize()
    {
      bool isInitialized = false;
      var stateFile = new FileInfo(constantApplicationStatePath);
      if (stateFile.Exists)
      {
        var json = File.ReadAllText(constantApplicationStatePath);
        if (!string.IsNullOrWhiteSpace(json))
        {
          try
          {
            ApplicationState = JsonConvert.DeserializeObject<ApplicationState>(json);
            isInitialized = true;
          }
          catch { }
        }
      }


      var solutionHelperCatalog =
        new FolderPluginCatalog(
          @"D:\Dev\Programming 2023\SolutionHelper\SolutionHelperControls\bin\Debug\net7.0-windows",
          type => { type.Inherits(typeof(UserControl)); });

      var nugetHelperCatalog =
        new FolderPluginCatalog(
          @"D:\Dev\Programming 2023\SolutionHelper\NugetHelperControls\bin\Debug\net7.0-windows",
          type => { type.Inherits(typeof(UserControl)); });

      var folderPluginCatalog = new CompositePluginCatalog(solutionHelperCatalog, nugetHelperCatalog);
      folderPluginCatalog.Initialize();
      var allPlugins = folderPluginCatalog.GetPlugins();
      foreach (Plugin? plugin in allPlugins)
      {
        var ctrl = Activator.CreateInstance(plugin);
        if (ctrl is IHelperControl helperControl)
          pluginMap.Add(helperControl.HelperControlType, plugin);
      }

      if (ApplicationState != null && isInitialized && ApplicationState.ControlStates.Count > 0)
      {
        foreach (var controlState in ApplicationState.ControlStates.Values)
        {
          if (controlState.HasValue)
          {

            if (Activator.CreateInstance(pluginMap[controlState.ControlType]) is not IHelperControl control)
              continue;

            control.InitializeFromJson(controlState.JsonState);
            Controls.Add(control);
            control.Updated += HelperControl_Updated;
          }

        }

        var usedControlTypes = new HashSet<string>();
        foreach (var control in Controls)
          usedControlTypes.Add(control.HelperControlType);

        foreach (var controlType in pluginMap.Keys)
          if (!usedControlTypes.Contains(controlType))
            CreateUninitializedControl(controlType);
      }
      else
      {
        foreach (var controlType in pluginMap.Keys)
          CreateUninitializedControl(controlType);

        var jsonState = JsonConvert.SerializeObject(ApplicationState);
        File.WriteAllText(constantApplicationStatePath, jsonState);
      }

      SelectedControl = Controls[0];
    }

    private void CreateUninitializedControl(string controlType)
    {
      if (Activator.CreateInstance(pluginMap[controlType]) is not IHelperControl control)
        return;

      control.Key = control.HelperControlType;
      ApplicationState.ControlStates[control.HelperControlType] = new ControlState
      {
        JsonState = control.GetJsonData(),
        ControlType= control.HelperControlType
      };

      Controls.Add(control);
      control.Updated += HelperControl_Updated;
    }


    private void HelperControl_Updated(object? sender, EventArgs e)
    {
      var helperControl = SelectedControl as IHelperControl;
      var json = helperControl.GetJsonData();

      ApplicationState.ControlStates[helperControl.Key].JsonState = json;

      var jsonState = JsonConvert.SerializeObject(ApplicationState);
      File.WriteAllText(constantApplicationStatePath, jsonState);
    }

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
