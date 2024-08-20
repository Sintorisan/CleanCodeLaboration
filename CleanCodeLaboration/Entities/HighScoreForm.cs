namespace CleanCodeLaboration.Entities;

public class HighScoreForm
{
    public string PlayerId { get; set; } = string.Empty;
    public int HighScore { get; set; }
    public string GameId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
