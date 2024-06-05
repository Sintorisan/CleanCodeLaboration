using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration.Games.Moo;

public class MooCartridge : ICartridge
{
    private readonly IGameLogic _gameLogic;
    private readonly IHighScoreService _highScore;
    private readonly IPlayerService _playerService;

    public MooCartridge(IGameLogic gameLogic)
    {
        _gameLogic = gameLogic;
        _highScore = new HighScoreService(gameLogic);
        _playerService = new PlayerService();
    }

    public void StartPlayerService(string playerName) => _playerService.InitialLoad(playerName);
    public IPlayer GetSingleIPlayer(string playerName) => _playerService.GetSinglePlayer(playerName);
    public bool isExistingPlayer(string playerName) => _playerService.isPlayerFound(playerName);


    public List<HighScoreForm> GetAllHighScores() => _highScore.GetAllHighScores().ToList();
    public List<HighScoreForm> GetAllPlayerHighScores(string playerName) => _highScore.GetAllUserHighScore(playerName).ToList();


    public string GetFirstDataStorage => _gameLogic.FirstDataStorage;
    public void SetFirstDataStorage(string data) => _gameLogic.FirstDataStorage = data;
    public string GetSecondDataStorage => _gameLogic.SecondDataStorage;
    public void SetSecondDataStorage(string data) => _gameLogic.SecondDataStorage = data;

    public void GameStartUp() => _gameLogic.GameStartUp();
    public void GamePlayLoop() => _gameLogic.GamePlayLoop();
    public void GameShutDown() => _gameLogic.GameShutDown();
}
