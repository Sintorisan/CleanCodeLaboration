using CleanCodeLaboration.Entities;

namespace CleanCodeLaboration.Factory;
public interface IHighScoreFactory
{
    HighScoreForm CreateHighScore(string gameId, string playerId, int score);
}
public class HighScoreFactory : IHighScoreFactory
{
    public HighScoreForm CreateHighScore(string gameId, string playerId, int score)
    {
        return new HighScoreForm
        {
            Date = DateTime.UtcNow,
            GameId = gameId,
            PlayerId = playerId,
            HighScore = score
        };
    }
}
