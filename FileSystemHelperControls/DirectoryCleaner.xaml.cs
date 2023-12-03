using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FileSystemHelper.Filters;
using FileSystemHelper.Filters.FileInfoFilters;
using Helper.Shared;
using Newtonsoft.Json;

namespace FileSystemHelperControls
{
  /// <summary>
  /// Interaction logic for DirectoryCleaner.xaml
  /// </summary>
  [JsonObject(MemberSerialization.OptIn)]
  public partial class DirectoryCleaner : UserControl, IHelperControl, INotifyPropertyChanged
  {
    private string directoryPath;

    public DirectoryCleaner()
    {
      InitializeComponent();
      Title = "Directory Cleaner";
      Key = $"{Assembly.GetExecutingAssembly().GetName().Name}:{HelperControlType}";
    }

    [JsonProperty]
    public string DirectoryPath
    {
      get { return directoryPath; }
      set
      {
        if (value == directoryPath)
          return;

        directoryPath = value;
        OnPropertyChanged();
      }
    }

    public string GetJsonData()
    {
      var output = JsonConvert.SerializeObject(this);
      return output;
    }

    public void InitializeFromJson(string json)
    {
      var item = JsonConvert.DeserializeObject<DirectoryCleaner>(json);

      if (item == null)
        return;

      Key = item.Key;
      DirectoryPath = item.DirectoryPath;
    }

    [JsonProperty]
    public string Title { get; set; }

    [JsonProperty]
    public string Key { get; set; }

    public ObservableCollection<FileInfo> Files { get; set; } = new();

    public string HelperControlType => nameof(DirectoryCleaner);
    public event EventHandler? Updated;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void FindFiles(object sender, RoutedEventArgs e)
    {
      Updated?.Invoke(sender, e);
      ProcessFindFiles();
    }

    private async void ProcessFindFiles()
    {

      var dir = new DirectoryInfo(DirectoryPath);

      if (!dir.Exists)
      {
        MessageBox.Show("Directory does not exist");
        return;
      }

      var files = await GetFilesAsync(dir);

      var andFilter = new AndFilter<FileInfo>();
      andFilter.Filters.Add(new ExtensionFilter  { Extensions = new List<string> { ".txt", ".html" } });

      var filteredFiles = await andFilter.FilterItemsAsync(files);
      Files.Clear();
      foreach (var file in filteredFiles)
        Files.Add(file);
    }

    public static async Task<FileInfo[]> GetFilesAsync(DirectoryInfo dir)
    {
      return await Task.Run(() => dir.GetFiles("*.*", SearchOption.AllDirectories));
    }

    private void DeleteAll(object sender, RoutedEventArgs e)
    {
      foreach (FileInfo file in Files)
        file.Delete();

      ProcessFindFiles();
    }

    private void DeleteSelect(object sender, RoutedEventArgs e)
    {
      foreach (FileInfo file in lstFiles.SelectedItems)
        file.Delete();

      ProcessFindFiles();
    }
  }
}
