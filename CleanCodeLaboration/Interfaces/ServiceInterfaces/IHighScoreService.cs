using CleanCodeLaboration.Entities;

namespace CleanCodeLaboration.Interfaces.ServiceInterfaces;

public interface IHighScoreService
{
    ICollection<HighScoreForm> GetAllHighScores();

    public void AddHighScore(HighScoreForm highScore);
}