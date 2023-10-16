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
      IsolatedStorageHelper.WriteToIsolatedStorage($"{IsolatedStoragePrefix}:fileDirectory", txtDirectory.Text.Trim());
      IsolatedStorageHelper.WriteToIsolatedStorage($"{IsolatedStoragePrefix}:outPutDirectory", txtOutputDirectory.Text.Trim());
    }

    public void LoadFromIsolatedStorage()
    {
       txtDirectory.Text = IsolatedStorageHelper.ReadFromIsolatedStorage($"{IsolatedStoragePrefix}:fileDirectory");
       txtOutputDirectory.Text = IsolatedStorageHelper.ReadFromIsolatedStorage($"{IsolatedStoragePrefix}:outPutDirectory");
    }

    private void btCreate_Click(object sender, RoutedEventArgs e)
    {
      SaveToIsolatedStorage();
      var packager = new NugetPack();
      packager.PackageFromProject(@"D:\Dev\Programming 2023\SampleCompositeSolution\SampleNuget\SampleDb\SampleDb.csproj", txtOutputDirectory.Text);
      //var command = """
      //              /K
      //              nuget.exe pack "D:\Dev\Programming 2023\SampleCompositeSolution\SampleNuget\SampleDb\SampleDb.csproj"
      //              """;
      //var startInfo = new ProcessStartInfo("cmd.exe")
      //{
      //  Arguments = command,
      //  UseShellExecute = false
      //};

      //var process = Process.Start(startInfo);
      //process?.WaitForExit();

      //process?.Kill();
    }
  }
}
