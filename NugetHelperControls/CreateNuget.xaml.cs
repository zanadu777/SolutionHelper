using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using GuiShared;
using NugetHelper;
using SolutionHelperControls;

namespace NugetHelperControls
{
  /// <summary>
  /// Interaction logic for CreateNuget.xaml
  /// </summary>
  public partial class CreateNuget : UserControl, IIsolatedStoragePersistent
  {
    public CreateNuget()
    {
      InitializeComponent();
    }

    private void CreateNuget_OnLoaded(object sender, RoutedEventArgs e)
    {
      LoadFromIsolatedStorage();
    }

    public string IsolatedStoragePrefix { get; set; }
    public void SaveToIsolatedStorage()
    {
      IsolatedStorageHelper.WriteToIsolatedStorage($"{IsolatedStoragePrefix}:projectPath", txtProjectPath.Text.Trim());
      IsolatedStorageHelper.WriteToIsolatedStorage($"{IsolatedStoragePrefix}:outPutDirectory", txtOutputDirectory.Text.Trim());
    }

    public void LoadFromIsolatedStorage()
    {
       txtProjectPath.Text = IsolatedStorageHelper.ReadFromIsolatedStorage($"{IsolatedStoragePrefix}:projectPath");
       txtOutputDirectory.Text = IsolatedStorageHelper.ReadFromIsolatedStorage($"{IsolatedStoragePrefix}:outPutDirectory");
    }

    private void btCreate_Click(object sender, RoutedEventArgs e)
    {
      SaveToIsolatedStorage();
      var packager = new NugetPack();
      packager.PackageFromProject(txtProjectPath.Text.Trim(), txtOutputDirectory.Text.Trim());
    }
  }
}
