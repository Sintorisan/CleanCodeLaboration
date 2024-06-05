namespace CleanCodeLaboration.Interfaces;

public interface IGameLogic
{
    string GameId { get; set; }
    string FirstDataStorage { get; set; }
    string SecondDataStorage { get; set; }
    int Score { get; set; }



    void GameStartUp();
    void GamePlayLoop();
    void GameShutDown();
}
