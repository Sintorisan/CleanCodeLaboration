using CleanCodeLaboration.Entities;

namespace CleanCodeLaboration.Interfaces;

public interface IHighScoreService
{
    ICollection<HighScoreForm> GetAllHighScores();
    ICollection<HighScoreForm> GetAllUserHighScore(string id);


}
