using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration;

public class GameplayController : IGameplayController
{
    private readonly IGameLogic _gameLogic;
    private readonly IPlayerService _playerService;
    private readonly IHighScoreIO _highScoreIO;

    public GameplayController(IGameLogic gameLogic, string playerName)
    {
        _gameLogic = gameLogic;
        _playerService = new PlayerService(playerName);
        _highScoreIO = new HighScoreIO(gameLogic, playerName);
    }

    public int Score
    {
        get => _gameLogic.Score;
        set => _gameLogic.Score = value;
    }

    public string FirstDataStorage
    {
        get => _gameLogic.FirstDataStorage;
        set => _gameLogic.FirstDataStorage = value;
    }

    public string SecondDataStorage
    {
        get => _gameLogic.SecondDataStorage;
        set => _gameLogic.SecondDataStorage = value;
    }

    public IPlayer GetSingleIPlayer(string playerName) => _playerService.GetSinglePlayer(playerName);

    public bool isExistingPlayer(string playerName) => _playerService.isPlayerFound(playerName);

    public IPlayer GetCurrentPlayer() => _playerService.GetCurrentPlayer();

    public void CreateAndAddNewHighScore()
    {
        var newHighScore = new HighScoreForm
        {
            Date = DateTime.UtcNow,
            GameId = _gameLogic.GameId,
            PlayerId = _playerService.GetCurrentPlayer().PlayerId,
            HighScore = _gameLogic.Score
        };

        _highScoreIO.AddNewHighScore(newHighScore);
    }

    public void RunHighScoreIO() => _highScoreIO.RunHighScoreIO();

    public void GameStartUp() => _gameLogic.GameStartUp();

    public void GamePlayLoop() => _gameLogic.GamePlayLoop();

    public void GameShutDown() => _gameLogic.GameShutDown();
}