using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.RockPaperScissors;

public class RockPaperScissorsGameIO : IGameIO
{
    private const string INVALID_CHOICE_MESSAGE = "Invalid input. Please try again.";
    private const int WINNING_SCORE_DIFFERENCE = 3;

    private readonly IGameplayController _controller;
    private int _playerPoints = 0;
    private int _cpuPoints = 0;
    private bool _isPlaying = true;
    private string[] _choices = { "Rock", "Paper", "Scissors" };

    private int TotalMoves => _playerPoints + _cpuPoints;
    public IPlayer Player => _controller.GetCurrentPlayer();
    public string ComputerHand => _controller.FirstDataStorage;
    public string PlayerHand => _controller.SecondDataStorage;
    public int Results => _controller.Score;

    public RockPaperScissorsGameIO(IGameplayController controller)
    {
        _controller = controller;
    }

    public void Run()
    {
        try
        {
            StartGame();
            GameLoop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            EndGame();
        }
    }

    private void StartGame()
    {
        GreetPlayer();
        _isPlaying = true;
    }

    private void GreetPlayer()
    {
        PrintScreen(WelcomeScreen(), false);

        var keyPressed = Console.ReadKey();
        if (keyPressed.Key == ConsoleKey.H)
        {
            _controller.RunHighScoreIO();
        }

        Console.Clear();
    }

    private void GameLoop()
    {
        PrintScreen(PlayingFieldScreen("Round 1"), false);
        do
        {
            PrepareOpponent();
            Console.WriteLine(ComputerHand);
            GetPlayersChoice();
            HandleResults();
        } while (_isPlaying);
    }

    private void PrepareOpponent()
    {
        _controller.GameStartUp();
    }

    private void GetPlayersChoice()
    {
        var playerInput = GetValidChoice();
        _controller.SecondDataStorage = playerInput;
    }

    private string GetValidChoice()
    {
        var input = Console.ReadLine();

        while (!ApprovedChoice(input))
        {
            Console.WriteLine(INVALID_CHOICE_MESSAGE);
            input = Console.ReadLine();
        }

        return input;
    }

    private bool ApprovedChoice(string input)
    {
        return _choices.Any(c => c.Equals(input, StringComparison.OrdinalIgnoreCase) ||
                                                input.Equals("q", StringComparison.OrdinalIgnoreCase));
    }

    private void HandleResults()
    {
        if (Quit())
        {
            _isPlaying = false;
            return;
        }

        _controller.GamePlayLoop();
        DisplayResult();

        if (HasPlayerWon())
        {
            PlayerWinner();
            _isPlaying = false;
        }
        else if (HasCPUWon())
        {
            CPUWinner();
            _isPlaying = false;
        }
    }

    private bool Quit() => PlayerHand.Equals("q", StringComparison.OrdinalIgnoreCase);

    private bool HasPlayerWon() => (_playerPoints - _cpuPoints) == WINNING_SCORE_DIFFERENCE;

    private bool HasCPUWon() => (_cpuPoints - _playerPoints) == WINNING_SCORE_DIFFERENCE;

    private void DisplayResult()
    {
        Console.Clear();

        switch (Results)
        {
            case -1:
                _cpuPoints++;
                PrintScreen(PlayingFieldScreen("Computers point!"), false);
                break;

            case 0:
                PrintScreen(PlayingFieldScreen("It's a tie!"), false);
                break;

            case 1:
                _playerPoints++;
                PrintScreen(PlayingFieldScreen("Your point!"), false);
                break;

            default:
                break;
        }
    }

    private void PlayerWinner()
    {
        PrintScreen(WinnerScreen(), false);

        var keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Y)
        {
            AddHighscore();
        }
        else
        {
            Console.WriteLine("\nYour score was not added to the High Score List.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }

    private void AddHighscore()
    {
        _controller.Score = TotalMoves;
        _controller.CreateAndAddNewHighScore();
    }

    private void CPUWinner()
    {
        PrintScreen(LoserScreen(), true);
    }

    private void EndGame()
    {
        PrintScreen(GoodbyeScreen(), true);
        _controller.GameShutDown();
    }

    private void PrintScreen(string screen, bool interaction)
    {
        Console.Clear();
        Console.WriteLine(screen);
        if (interaction)
        {
            Console.ReadKey();
        }
    }

    private string WelcomeScreen()
    {
        return @$"
___________________________________________
|                                         |
|*****************************************|
|*                                       *|
|*      W E L C O M E   T O   T H E      *|
|*                                       *|
|*       E P I C  B A T T L E  O F       *|
|*                                       *|
|*          R O C K , P A P E R          *|
|*           S C I S S O R S !           *|
|*****************************************|
|                                         |
|{Player.PlayerId,22}!                  |
| Welcome to the ultimate showdown of     |
| Rock, Paper, and Scissors!              |
|                                         |
| Here are some ground rules for the game:|
| - Type in your choice: Rock, Paper,     |
|   or Scissors.                          |
| - To win, you must lead by 3 points.    |
| - Your final score is based on how few  |
|   moves it takes you to beat the        |
|   computer.                             |
|                                         |
| To see high scores press 'h', or press  |
| any other key to start a new game!      |
|_________________________________________|
";
    }

    private string PlayingFieldScreen(string result)
    {
        return
@$"
___________________________________________
|      Player               Computer      |
|{_playerPoints,10}{_cpuPoints,22}         |
|                                         |
|       Hand        VS        Hand        |
|                                         |
|{PlayerHand,11}{ComputerHand,22}        |
|                                         |
|{result,26}               |
|_________________________________________|
 Choose you hand: Rock, Paper or Scissors";
    }

    private string GoodbyeScreen()
    {
        return @$"
___________________________________________
|                                         |
|*****************************************|
|*                                       *|
|*       T H A N K   Y O U   F O R       *|
|*                                       *|
|*           P L A Y I N G ! !           *|
|*                                       *|
|*****************************************|
|                                         |
|              See you soon!              |
|_________________________________________|
";
    }

    private string LoserScreen()
    {
        return
    @$"
___________________________________________
|                                         |
|*****************************************|
|*                                       *|
|*          W E L L ,  W E L L ,         *|
|*                                       *|
|*          L O O K S  L I K E           *|
|*                                       *|
|*    T H E  C O M P U T E R  W O N !    *|
|*                                       *|
|*****************************************|
|                                         |
|    Maybe it's time to practice those    |
|          Rock, Paper, Scissors          |
|            skills a bit more!           |
|                                         |
|      Press any key to cry in peace.     |
|_________________________________________|
";
    }

    private string WinnerScreen()
    {
        return
@$"
___________________________________________
|                                         |
|*****************************************|
|*                                       *|
|*     C O N G R A T U L A T I O N S     *|
|*                                       *|
|*{Player.PlayerId.ToUpper(),24}               *|
|*             Y O U   W O N!            *|
|*                                       *|
|*****************************************|
|                                         |
|         Your Score: {TotalMoves} points!           |
|                                         |
|   Would you like to add your score to   |
|          the High Score List?           |
|    Press 'Y' for Yes or any other key   |
|              to continue...             |
|_________________________________________|
";
    }
}