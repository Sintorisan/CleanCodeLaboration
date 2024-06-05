using CleanCodeLaboration.Games.Moo;
using CleanCodeLaboration.Interfaces;

namespace CleanCodeLaboration;

public class Dashboard
{
    private readonly Dictionary<int, string> _games;
    private bool _isPlaying = true;
    public Dashboard()
    {
        _games = new Dictionary<int, string>{
            { 1, "Cows N Bulls" },
        };
    }


    public void Run()
    {
        do
        {
            var playerInput = PlayerInteraction();
            if (_isPlaying)
            {
                var gameToPlay = ChooseAGameToAssemble(playerInput);
                StartGame(gameToPlay);
            }
        } while (_isPlaying);
    }


    private string PlayerInteraction()
    {
        Console.WriteLine("Welcome to the party!\nHere's the games to choose from.");
        DisplayAllGames();

        Console.Write("Please choose a game to your likings: ");
        var playerInput = GetPlayerInput();

        return playerInput;
    }

    #region PlayerInteraction-------------------------------
    private void DisplayAllGames()
    {
        foreach (var game in _games)
        {
            Console.WriteLine($"{game.Key} - {game.Value}");
        }
        Console.WriteLine("q - Quit");
    }
    private string GetPlayerInput()
    {
        bool isNotValidInput = true;
        var playerInput = string.Empty;

        do
        {
            playerInput = Console.ReadLine();
            isNotValidInput = HandleValidation(playerInput);
        } while (isNotValidInput);

        return playerInput;
    }
    private bool HandleValidation(string input)
    {
        int howManyGames = _games.Count;

        if (input == "q")
        {
            TurnOff();
            return false;
        }

        for (int i = 1; i <= howManyGames; i++)
        {
            if (i.ToString() == input || input == "q")
                return false;
        }

        Console.WriteLine("Invalid input! Please try again");
        return true;
    }
    void TurnOff()
    {
        Console.WriteLine("Bye and thank you for playing!");
        Console.ReadKey();
        _isPlaying = false;
    }

    #endregion----------------------------------------------


    private ICartridge ChooseAGameToAssemble(string gameLogic)
    {
        var assembledCartridge = AssembleCartridge(gameLogic);

        return assembledCartridge;
    }

    #region ChooseAGameToPlay ---------------
    private ICartridge AssembleCartridge(string gameLogicName)
    {
        var gameLogic = GetGameLogic(gameLogicName);

        return new MooCartridge(gameLogic);
    }
    IGameLogic GetGameLogic(string game)
    {
        return game switch
        {
            "1" => new MooGameLogic(),
            _ => new MooGameLogic(),
        };
    }
    #endregion --------------------

    private void StartGame(ICartridge game)
    {
        MooGameEngine engine = new MooGameEngine(game);

        engine.RunConsoleGame();
    }






}
