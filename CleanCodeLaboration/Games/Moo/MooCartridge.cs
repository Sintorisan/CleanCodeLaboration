using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Factory;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration.Games.Moo;

// Facade pattern som slår ihop logiken men alla tjänster för att kunna köra i konsollen

public class MooCartridge : ICartridge
{
    private readonly IGameLogic _gameLogic;
    private readonly IHighScoreService _highScore;
    private readonly IPlayerService _playerService;
    private readonly HighScoreFactory _highScoreFactory = new HighScoreFactory();


    public MooCartridge(IGameLogic gameLogic, string playerName)
    {
        _gameLogic = gameLogic;
        _highScore = new HighScoreService(gameLogic);
        _playerService = new PlayerService(playerName);
    }



    public IPlayer GetSingleIPlayer(string playerName) => _playerService.GetSinglePlayer(playerName);
    public bool isExistingPlayer(string playerName) => _playerService.isPlayerFound(playerName);
    public IPlayer GetCurrentPlayer() => _playerService.GetCurrentPlayer();


    public void CreateNewHighScore()
    {
        var game = _gameLogic.GameId;
        var player = _playerService.GetCurrentPlayer();
        var score = _gameLogic.Score;

        var highScore = _highScoreFactory.CreateHighScore(game, player.PlayerId, score);

        _highScore.AddHighScore(highScore);
    }

    public HighScoreForm GetHighestScore(string playerName) => _highScore.GetHighestPlayerScore(playerName);
    public List<HighScoreForm> GetAllHighScores() => _highScore.GetAllHighScores().ToList();
    public List<HighScoreForm> GetAllPlayerHighScores(string playerName) => _highScore.GetAllUserHighScore(playerName).ToList();


    public string GetFirstDataStorage => _gameLogic.FirstDataStorage;
    public void SetFirstDataStorage(string data) => _gameLogic.FirstDataStorage = data;
    public string GetSecondDataStorage => _gameLogic.SecondDataStorage;
    public void SetSecondDataStorage(string data) => _gameLogic.SecondDataStorage = data;
    public int GetCurrentScore() => _gameLogic.Score;


    public void GameStartUp() => _gameLogic.GameStartUp();
    public void GamePlayLoop() => _gameLogic.GamePlayLoop();
    public void GameShutDown() => _gameLogic.GameShutDown();
}
