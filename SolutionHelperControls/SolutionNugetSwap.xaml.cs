using System;
using System.Collections.Generic;
using System.ComponentModel;
using SolutionHelper;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Helper.Shared;
using Newtonsoft.Json;
using System.Reflection;

namespace SolutionHelperControls
{
  /// <summary>
  /// Interaction logic for SolutionNugetSwap.xaml
  /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
  public partial class SolutionNugetSwap : UserControl, IHelperControl, INotifyPropertyChanged
  {
    private string solutionPath;
    private string? nugetSolutionPath;

    public SolutionNugetSwap()
    {
      InitializeComponent();
      Title = "Solution Nuget Swap";
      Key = $"{Assembly.GetExecutingAssembly().GetName().Name}:{HelperControlType}";
    }

    private void SolutionNugetSwap_OnLoaded(object sender, RoutedEventArgs e)
    {
      //LoadFromIsolatedStorage();
    }
    [JsonProperty]
    public string Title { get; set; }

    [JsonProperty]
    public string Key { get; set; }

    [JsonProperty]
    public string HelperControlType => "SolutionNugetSwap";

    [JsonProperty]
    public string? SolutionPath
    {
      get => solutionPath;
      set
      {
        if (value == solutionPath) return;
        solutionPath = value;
        OnPropertyChanged();
      }
    }

    [JsonProperty]
    public string? NugetSolutionPath
    {
      get => nugetSolutionPath;
      set
      {
        if (value == nugetSolutionPath) return;
        nugetSolutionPath = value;
        OnPropertyChanged();
      }
    }

    public string GetJsonData()
    {
      var output =JsonConvert.SerializeObject(this);
      return output;
    }

    public void InitializeFromJson(string json)
    {
      var item = JsonConvert.DeserializeObject<SolutionNugetSwap>(json);

      if (item == null)
        return;

      Key = item.Key;
      SolutionPath = item.SolutionPath;
      nugetSolutionPath = item.NugetSolutionPath;
    }

    public event EventHandler? Updated;

    private void Analyze(object sender, RoutedEventArgs e)
    {
      Updated?.Invoke(sender, e);

      var fi = new FileInfo(txtSolutionPath.Text);
      if (fi.Exists)
      {
        //SaveToIsolatedStorage();
        var soln = new VisualStudioSolution(fi);

        txtDetails.Text = soln.Details();
      }
      else
        MessageBox.Show($"{txtSolutionPath.Text} does not exist");
    }

    public string IsolatedStoragePrefix { get; set; }

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
