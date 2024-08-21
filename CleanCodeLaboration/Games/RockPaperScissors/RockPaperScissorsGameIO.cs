using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.RockPaperScissors;

// Spelets UI.

public class RockPaperScissorsGameIO : IGameIO
{
    private const string INVALID_CHOICE = "Invalid input. Please try again.";

    private readonly IGameplayController _controller;
    private int _playerScore;
    private int _cpuScore;
    private bool _isPlaying = true;
    private bool _isVictorious = false;
    private string[] _choices = { "Rock", "Paper", "Scissors" };


    public IPlayer Player => _controller.GetCurrentPlayer();
    public string ComputerHand => _controller.GetFirstDataStorage;
    public string PlayerHand => _controller.GetSecondDataStorage;
    public int Results => _controller.GetCurrentScore();

    public RockPaperScissorsGameIO(IGameplayController controller)
    {
        _controller = controller;
    }

    public void Run()
    {
        GreetPlayer();

        do
        {
            PrepareGame();
            Console.WriteLine(ComputerHand);
            GetPlayersChoice();
            DisplayResult();
        } while (GameIsActive());

        EndGame();
        Console.Clear();
    }

    private void GreetPlayer()
    {
        Console.WriteLine($"Hello {Player.PlayerId}, and welcome to Rock Paper Scissors!\n");
        Console.WriteLine("Press any key to start a new game!");
        Console.ReadKey();
        Console.Clear();
    }

    private void PrepareGame()
    {
        _isPlaying = true;
        _controller.GameStartUp();
    }

    private void GetPlayersChoice()
    {
        var playerInput = GetValidChoice();
        _controller.SetSecondDataStorage(playerInput);
    }

    private string GetValidChoice()
    {
        string input = Console.ReadLine();

        while (!ApprovedChoice(input))
        {
            Console.WriteLine(INVALID_CHOICE);
            input = Console.ReadLine();
        }

        return input;
    }

    private bool ApprovedChoice(string input)
    {
        return _choices.Any(c => c.Equals(input, StringComparison.OrdinalIgnoreCase));
    }

    private void DisplayResult()
    {
        throw new NotImplementedException();
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }

    private bool GameIsActive()
    {
        return true;
    }
}