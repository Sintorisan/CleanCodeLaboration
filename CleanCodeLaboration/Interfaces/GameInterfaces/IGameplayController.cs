namespace CleanCodeLaboration.Interfaces.GameInterfaces;

public interface IGameplayController
{
    public string FirstDataStorage { get; set; }
    public string SecondDataStorage { get; set; }
    public int Score { get; set; }

    IPlayer GetSingleIPlayer(string playerName);

    public bool isExistingPlayer(string playerName);

    public IPlayer GetCurrentPlayer();

    public void CreateAndAddNewHighScore();

    public void RunHighScoreIO();

    public void GameStartUp();

    public void GamePlayLoop();

    public void GameShutDown();
}