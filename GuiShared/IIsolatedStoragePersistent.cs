namespace GuiShared
{
  public interface IIsolatedStoragePersistent
  {
    string IsolatedStoragePrefix { get; set; }

    void SaveToIsolatedStorage();
    void LoadFromIsolatedStorage();
  }
}
