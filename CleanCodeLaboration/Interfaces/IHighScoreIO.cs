using CleanCodeLaboration.Entities;

namespace CleanCodeLaboration.Interfaces;

public interface IHighScoreIO
{
    public void RunHighScoreIO();

    public void AddNewHighScore(HighScoreForm newHighScore);
}