using System.Windows;
using SolutionHelperWpf;

namespace Helper
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      var vm = new HelpersVm();
      vm.Initialize();
      DataContext = vm;
    }
  }
}

