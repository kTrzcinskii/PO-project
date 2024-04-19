namespace FlightManager.DataUpdater;

internal interface IDataUpdater
{
    public void StartUpdateLoop(string sourcePath);
}