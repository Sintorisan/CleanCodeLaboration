using CleanCodeLaboration.Db;
using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces.DbInterfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;

namespace CleanCodeLaboration.Services;

public class HighScoreService : IHighScoreService
{
    private readonly IGameLogic _game;
    private readonly IDatabase<HighScoreForm> _database;

    public HighScoreService(IGameLogic game)
    {
        _game = game;
        _database = new HighScoreDb(game.GameId);
        _database.InitialLoad();
    }

    public void AddHighScore(HighScoreForm highScore) => _database.Add(highScore);

    public ICollection<HighScoreForm> GetAllHighScores() => _database.GetAll();
}