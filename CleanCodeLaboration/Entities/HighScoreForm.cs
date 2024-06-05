namespace CleanCodeLaboration.Entities;

public class HighScoreForm
{
    public string PlayerId { get; set; } = string.Empty;
    public string HighScore { get; set; } = string.Empty;
    public string GameId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
