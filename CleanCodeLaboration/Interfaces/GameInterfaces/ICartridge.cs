using CleanCodeLaboration.Entities;

namespace CleanCodeLaboration.Interfaces.GameInterfaces;

public interface ICartridge
{
    public string GetFirstDataStorage { get; }
    public string GetSecondDataStorage { get; }
    void SetFirstDataStorage(string data);
    void SetSecondDataStorage(string data);


    IPlayer GetSingleIPlayer(string playerName);
    public bool isExistingPlayer(string playerName);
    public IPlayer GetCurrentPlayer();
    public int GetCurrentScore();

    public void CreateNewHighScore();
    public HighScoreForm GetHighestScore(string playerName);
    public List<HighScoreForm> GetAllHighScores();
    public List<HighScoreForm> GetAllPlayerHighScores(string playerName);



    public void GameStartUp();
    public void GamePlayLoop();
    public void GameShutDown();


}
