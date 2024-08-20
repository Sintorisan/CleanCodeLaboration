using CleanCodeLaboration.Entities;

namespace CleanCodeLaboration.Interfaces.ServiceInterfaces;

public interface IHighScoreService
{
    HighScoreForm GetHighestPlayerScore(string playerId);
    ICollection<HighScoreForm> GetAllHighScores();
    ICollection<HighScoreForm> GetAllUserHighScore(string id);
    public void AddHighScore(HighScoreForm highScore);

}
