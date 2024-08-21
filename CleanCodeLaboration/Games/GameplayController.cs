using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration.Games;

// Använder "Facade pattern" för att slår ihop logiken men alla tjänster för att kunna köra i konsollen

public class GameplayController : IGameplayController
{
    private readonly IGameLogic _gameLogic;
    private readonly IHighScoreService _highScore;
    private readonly IPlayerService _playerService;

    public GameplayController(IGameLogic gameLogic, string playerName)
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
        var newHighScore = new HighScoreForm
        {
            Date = DateTime.UtcNow,
            GameId = _gameLogic.GameId,
            PlayerId = _playerService.GetCurrentPlayer().PlayerId,
            HighScore = _gameLogic.Score
        };

        _highScore.AddHighScore(newHighScore);
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
