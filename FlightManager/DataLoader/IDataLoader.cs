namespace FlightManager.DataLoader;
internal interface IDataLoader
{
    public void Load(string dataPath);

    public delegate void OnDataLoaded(object sender, EventArgs args);

    public event OnDataLoaded DataLoaded;
}
