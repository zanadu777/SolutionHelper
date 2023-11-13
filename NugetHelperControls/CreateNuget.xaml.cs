using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Helper.Shared;
using Newtonsoft.Json;
using NugetHelper;

namespace NugetHelperControls
{
  /// <summary>
  /// Interaction logic for CreateNuget.xaml
  /// </summary>
  public partial class CreateNuget : UserControl, IHelperControl, INotifyPropertyChanged
  {
    public CreateNuget()
    {
      InitializeComponent();
      Title = "Create Nuget";
      Key = $"{Assembly.GetExecutingAssembly().GetName().Name}:{HelperControlType}";
    }

    private void CreateNuget_OnLoaded(object sender, RoutedEventArgs e)
    {
      
      
    }

    public string IsolatedStoragePrefix { get; set; }


    private void btCreate_Click(object sender, RoutedEventArgs e)
    {
      Updated?.Invoke(sender, e);

      var packager = new NugetPack();
      packager.PackageFromProject(txtProjectPath.Text.Trim(), txtOutputDirectory.Text.Trim());
    }

    public string Title { get; set; }
    public string Key { get; set; }
    public string HelperControlType => "CreateNuget";
    public string GetJsonData()
    {
      var props = new CreateNugetProperties
      {
        OutputDirectory = txtOutputDirectory.Text.Trim(),
        PathToProject = txtProjectPath.Text.Trim()
      };
     return JsonConvert.SerializeObject(props);
    }

    public void InitializeFromJson(string json)
    {
      var state = JsonConvert.DeserializeObject<CreateNugetProperties>(json);
      txtOutputDirectory.Text = state?.OutputDirectory;
      txtProjectPath.Text = state?.PathToProject;

    }

    public event EventHandler? Updated;
    public event PropertyChangedEventHandler? PropertyChanged;
  }
}
