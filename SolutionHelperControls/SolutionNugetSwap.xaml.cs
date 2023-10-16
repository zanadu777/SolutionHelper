using SolutionHelper;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using GuiShared;

namespace SolutionHelperControls
{
  /// <summary>
  /// Interaction logic for SolutionNugetSwap.xaml
  /// </summary>
  public partial class SolutionNugetSwap : UserControl, IIsolatedStoragePersistent
  {
    public SolutionNugetSwap()
    {
      InitializeComponent();
    }

    private void SolutionNugetSwap_OnLoaded(object sender, RoutedEventArgs e)
    {
      LoadFromIsolatedStorage();
    }

    private void Analyze(object sender, RoutedEventArgs e)
    {
      var fi = new FileInfo(txtSolutionPath.Text);
      if (fi.Exists)
      {
        SaveToIsolatedStorage();
        var soln = new VisualStudioSolution(fi);

        txtDetails.Text = soln.Details();
      }
      else
        MessageBox.Show($"{txtSolutionPath.Text} does not exist");
    }

    public string IsolatedStoragePrefix { get; set; }
    public void SaveToIsolatedStorage()
    {
      IsolatedStorageHelper.WriteToIsolatedStorage($"{IsolatedStoragePrefix}:solutionPath", txtSolutionPath.Text.Trim());
      IsolatedStorageHelper.WriteToIsolatedStorage($"{IsolatedStoragePrefix}:nugetSolutionPath", txtNugetSolutionPath.Text.Trim());
    }

    public void LoadFromIsolatedStorage()
    {
      txtSolutionPath.Text = IsolatedStorageHelper.ReadFromIsolatedStorage($"{IsolatedStoragePrefix}:solutionPath"); ;
      txtNugetSolutionPath.Text = IsolatedStorageHelper.ReadFromIsolatedStorage($"{IsolatedStoragePrefix}:nugetSolutionPath"); ;
    }
  }
}
