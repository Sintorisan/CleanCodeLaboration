using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces.DbInterfaces;

namespace CleanCodeLaboration.Db;

public class HighScoreDb : IDatabase<HighScoreForm>
{
    private List<HighScoreForm> _highScores = new List<HighScoreForm>();
    private readonly string _filePath;

    public HighScoreDb(string gameId)
    {
        _filePath = $"{gameId}.csv";
        InitialLoad();
    }

    public void InitialLoad()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var highScoresTable = File.ReadAllLines(_filePath);

                foreach (var hs in highScoresTable)
                {
                    var data = hs.Split(',');
                    _highScores.Add(new HighScoreForm
                    {
                        PlayerId = data[0],
                        HighScore = int.Parse(data[1]),
                        GameId = data[2],
                        Date = DateTime.Parse(data[3])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to initialize the high score database.", ex);
        }
    }

    public void Add(HighScoreForm highScore)
    {
        try
        {
            _highScores.Add(highScore);
            File.AppendAllText(_filePath, $"{highScore.PlayerId},{highScore.HighScore},{highScore.GameId},{highScore.Date}\n");
        }
        catch (Exception ex)
        {
            throw new Exception("Player could not be added.", ex);
        }
    }

    public List<HighScoreForm> GetAll() => _highScores;
}