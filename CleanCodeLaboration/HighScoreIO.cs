using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration;

public class HighScoreIO
{
    private readonly IHighScoreService _highScoreService;
    private readonly List<HighScoreForm> _highScoreList;
    private readonly IGameLogic _gameLogic;
    private readonly string _player;

    public HighScoreIO(IGameLogic gameLogic, string player)
    {
        _highScoreService = new HighScoreService(gameLogic);
        _highScoreList = _highScoreService.GetAllHighScores().ToList();
        _gameLogic = gameLogic;
        _player = player;
    }

    public List<HighScoreForm> GetAllHighScores() =>
        _highScoreList
        .OrderBy(hs => hs.HighScore)
        .ToList();

    public List<HighScoreForm> GetAllIndividualHighScores(string player) =>
        _highScoreList
        .Where(c => c.PlayerId == player)
        .OrderBy(hs => hs.HighScore)
        .ToList();

    public void RunHighScoreIO()
    {
        string[] options = { "All High Scores", "Your High Scores", "Exit" };
        int selectedIndex = 0;

        while (true)
        {
            DisplayMenu(options, selectedIndex);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = selectedIndex == 0 ? options.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = selectedIndex == options.Length - 1 ? 0 : selectedIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    ExecuteOption(selectedIndex);
                    if (selectedIndex == options.Length - 1) return;
                    break;
            }
        }
    }

    private static void DisplayMenu(string[] options, int selectedIndex)
    {
        Console.Clear();
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.Write("=> ");
            }
            else
            {
                Console.Write("   ");
            }

            Console.WriteLine(options[i]);
        }
    }

    private void ExecuteOption(int selectedIndex)
    {
        Console.Clear();

        switch (selectedIndex)
        {
            case 0:
                DisplayAllHighScores();
                break;

            case 1:
                DisplayAllIndividualHighScores(_player);
                break;

            case 2:
                return;
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public void DisplayAllHighScores()
    {
        foreach (var hs in GetAllHighScores())
        {
            Console.WriteLine($"{hs.PlayerId} - {hs.HighScore} - {hs.Date.ToString()}");
        }
    }

    public void DisplayAllIndividualHighScores(string player)
    {
        foreach (var hs in GetAllIndividualHighScores(player))
        {
            Console.WriteLine($"{hs.PlayerId} - {hs.HighScore} - {hs.Date.ToString()}");
        }
    }

    public void AddNewHighScore(HighScoreForm newHighScore)
    {
        _highScoreService.AddHighScore(newHighScore);
        DisplayHighScoreAddedMessage();
    }

    private static void DisplayHighScoreAddedMessage()
    {
        var confirmationScreen =
@$"
___________________________________________
|                                         |
|                                         |
|           Your score has been           |
|           successfully added!           |
|                                         |
|         Press any key to return         |
|           to the main menu...           |
|                                         |
|_________________________________________|
";
        Console.Clear();
        Console.WriteLine(confirmationScreen);
        Console.ReadKey();
    }
}