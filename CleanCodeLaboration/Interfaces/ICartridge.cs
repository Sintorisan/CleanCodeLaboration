namespace CleanCodeLaboration.Interfaces;

public interface ICartridge
{
    public string GetFirstDataStorage { get; }
    public string GetSecondDataStorage { get; }


    void SetFirstDataStorage(string data);
    void SetSecondDataStorage(string data);
    IPlayer GetSingleIPlayer(string playerName);
    public bool isExistingPlayer(string playerName);
    public void StartPlayerService(string playerName);
    public void GameStartUp();
    public void GamePlayLoop();
    public void GameShutDown();

}
