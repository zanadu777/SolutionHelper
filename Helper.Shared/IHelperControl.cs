namespace Helper.Shared
{
  public interface IHelperControl
  {
    string Title { get; set; }

    string Key { get; set; }

    string HelperControlType { get; }

    string GetJsonData();

    void InitializeFromJson(string json);

    event EventHandler Updated ;
  }
}
