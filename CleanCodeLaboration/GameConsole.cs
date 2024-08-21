using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Services;

namespace CleanCodeLaboration;

public class GameConsole
{
    private const string GAME_ERROR_MESSAGE = "Game not found!";
    private const string INVALID_CHOICE = "Invalid input. Please try again.";

    private readonly GameIOBuilder _gameEngineBuilderService = new GameIOBuilder();
    private readonly Dictionary<int, string> _gamesMap;
    private bool _isPlaying = true;
    private string _playerName = string.Empty;

    public GameConsole()
    {
        _gamesMap = new Dictionary<int, string>{
            { 1, "Cows N Bulls" },
            { 2, "Rock Paper Scissors" }
        };
    }

    public void Run()
    {
        Console.WriteLine("Welcome to Sindri's super awesome gaming console!");

        PlayerInteraction();

        while (_isPlaying)
        {
            HandlePlayerGameChoice();
        };
    }

    private void PlayerInteraction()
    {
        Console.Write("Who is playing?: ");
        _playerName = GetValidInput();

        Console.Clear();

        Console.WriteLine($"Welcome to the party {_playerName}!\nHere's the games to choose from.\n");
        DisplayAllGames();

        Console.Write("Please choose the number of the game to play: ");
    }

    private string GetValidInput()
    {
        string input = Console.ReadLine();

        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine(INVALID_CHOICE);
            input = Console.ReadLine();
        }

        return input;
    }

    private void DisplayAllGames()
    {
        foreach (var game in _gamesMap)
        {
            Console.WriteLine($"{game.Key} - {game.Value}");
        }
        Console.WriteLine("\nq - Quit");
    }

    private void HandlePlayerGameChoice()
    {
        while (true)
        {
            string playerInput = GetValidInput();

            if (PlayerWantsToQuit(playerInput))
            {
                TurnOff();
                break;
            }

            if (int.TryParse(playerInput, out int gameToPlay) &&
                gameToPlay >= 1 && gameToPlay <= _gamesMap.Count)
            {
                var assembledGame = AssembleGame(gameToPlay);
                StartGame(assembledGame);
                break;
            }
            else
            {
                Console.WriteLine(INVALID_CHOICE);
            }
        }
    }

    private bool PlayerWantsToQuit(string input)
    {
        return input.ToLower().Equals("q");
    }

    private void TurnOff()
    {
        Console.Clear();
        Console.WriteLine("Bye and thank you for playing!");
        Console.ReadKey();
        _isPlaying = false;
    }

    private IGameIO? AssembleGame(int game)
    {
        return game switch
        {
            1 => _gameEngineBuilderService.BuildMoo(_playerName),
            2 => _gameEngineBuilderService.BuildRockPaperScissors(_playerName),
            _ => null
        };
    }

    private void StartGame(IGameIO? game)
    {
        Console.Clear();

        if (game is not null)
        {
            game.Run();
        }
        else
        {
            Console.WriteLine(GAME_ERROR_MESSAGE);
        }
    }
}