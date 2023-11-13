using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using Helper.Shared;
using Newtonsoft.Json;

namespace NugetHelperControls
{
  /// <summary>
  /// Interaction logic for UpdateNuget.xaml
  /// </summary>
  public partial class UpdateNuget : UserControl, IHelperControl, INotifyPropertyChanged
  {
    public UpdateNuget()
    {
      InitializeComponent();
      Title = "Update Nuget";
      Key = $"{Assembly.GetExecutingAssembly().GetName().Name }:{HelperControlType}"; 
    }

    public string Title { get; set; }
    public string Key { get; set; }
    public string HelperControlType => "UpdateNuget";
    public string GetJsonData()
    {
      var props = new UpdateNugetProperties()
      {
        NugetLocation = txtNugetLocation.Text,
      };
      return JsonConvert.SerializeObject(props);
    }

    public void InitializeFromJson(string json)
    {
      var state = JsonConvert.DeserializeObject<UpdateNugetProperties>(json);
      txtNugetLocation.Text = state?.NugetLocation;
    }

    public event EventHandler? Updated;
    public event PropertyChangedEventHandler? PropertyChanged;

    private void btUpdate_Click(object sender, RoutedEventArgs e)
    {
      Updated?.Invoke(sender, e);
    }
  }
}
