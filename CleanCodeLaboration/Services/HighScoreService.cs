using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;

namespace CleanCodeLaboration.Services;

public class HighScoreService : IHighScoreService
{
    private readonly IGameLogic _game;
    private readonly List<HighScoreForm> _highScores = new List<HighScoreForm>();
    private string _filePath;

    public HighScoreService(IGameLogic game)
    {
        _game = game;
        _filePath = $"{_game.GameId}.csv";
        InitialLoadHighScores();
    }

    //Ska vara en låtsas db.
    //Bryter ner en .csv fil för att fylla på spelarens datatyp och returnerar en lista.
    private void InitialLoadHighScores()
    {
        if (File.Exists(_filePath))
        {
            var highScoreTable = File.ReadAllLines(_filePath);
            foreach (var highScore in highScoreTable)
            {
                var data = highScore.Split(',');
                var createHighScore = new HighScoreForm
                {
                    PlayerId = data[0],
                    HighScore = int.Parse(data[1])
                };
                _highScores.Add(createHighScore);
            }
        }
    }

    public void AddHighScore(HighScoreForm highScore)
    {
        _highScores.Add(highScore);
        File.AppendAllText(_filePath, $"{highScore.PlayerId},{highScore.HighScore}\n");
    }

    public ICollection<HighScoreForm> GetAllHighScores() => _highScores;
}