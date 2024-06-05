namespace CleanCodeLaboration;

public class Dashboard
{
    private readonly List<string> _games;

    public Dashboard()
    {
        _games = new List<string>{
            { "" },
            { "" }
        };
    }
    public void Run()
    {
        var gameToPlay = ChooseAGameToPlay();
        StartGame();
    }

    private void StartGame(string game)
    {

    }

    private string ChooseAGameToPlay()
    {
        DisplayAllGames();
        Console.WriteLine("Choose a game to play:");
        var playerInput = Console.ReadLine();

        switch (playerInput)
        {
            case "1":
                return _games[0];
            case "2":
                return _games[1];
            default:
                break;
        }
        return " ";
    }

    public void DisplayAllGames()
    {
        for (int i = 0; i < _games.Count; i++)
        {
            Console.WriteLine($"{i + 1} {_games[i]}");
        }
    }
}
