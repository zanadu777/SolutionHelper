using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolutionHelper;

namespace SolutionHelperWpf
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }



    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      txtSolutionPath.Text = IsolatedStorageHelper.ReadFromIsolatedStorage("solutionPath"); ;
      txtNugetSolutionPath.Text= IsolatedStorageHelper.ReadFromIsolatedStorage("nugetSolutionPath"); ;

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      

      var fi = new FileInfo(txtSolutionPath.Text);
      if (fi.Exists)
      {
        IsolatedStorageHelper.WriteToIsolatedStorage("solutionPath", txtSolutionPath.Text.Trim());
        IsolatedStorageHelper.WriteToIsolatedStorage("nugetSolutionPath", txtNugetSolutionPath.Text.Trim());
        var soln = new VisualStudioSolution(fi);

        txtDetails.Text = soln.Details();
      }
      else
        MessageBox.Show($"{txtSolutionPath.Text} does not exist");
   
    }
  }
}
