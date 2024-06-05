using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;

namespace CleanCodeLaboration.Services;

public class HighScoreService : IHighScoreService
{
    private readonly IGameLogic _game;
    private readonly List<HighScoreForm> _highScores = new List<HighScoreForm>();
    private string _gameHighScoreFilePath;


    public HighScoreService(IGameLogic game)
    {
        _game = game;
        _gameHighScoreFilePath = $"{_game.GameId}.csv";
        InitialLoadHighScores();
    }

    // Läser av filen
    private void InitialLoadHighScores()
    {
        if (File.Exists(_gameHighScoreFilePath))
        {
            var highScoreTable = File.ReadAllLines(_gameHighScoreFilePath);
            foreach (var highScore in highScoreTable)
            {
                var data = highScore.Split(',');
                var createHighScore = new HighScoreForm
                {
                    PlayerId = data[0],
                    HighScore = data[1]
                };
                _highScores.Add(createHighScore);
            }
        }
    }

    public void AddHighScore(HighScoreForm highScore)
    {
        _highScores.Add(highScore);
        File.AppendAllText(_gameHighScoreFilePath, $"{highScore.PlayerId},{highScore.HighScore}\n");
    }

    public ICollection<HighScoreForm> GetAllHighScores() => _highScores;

    public ICollection<HighScoreForm> GetAllUserHighScore(string id) => _highScores.FindAll(h => h.PlayerId == id);

}
