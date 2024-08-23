using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.RockPaperScissors;

// Spelets UI.

public class RockPaperScissorsGameIO : IGameIO
{
    private const string INVALID_CHOICE = "Invalid input. Please try again.";
    private const int WINNING_SCORE_DIFFERENCE = 3;

    private readonly IGameplayController _controller;
    private int _playerPoints = 0;
    private int _cpuPoints = 0;
    private bool _isPlaying = true;
    private string[] _choices = { "Rock", "Paper", "Scissors" };
    private int totalMoves => _playerPoints + _cpuPoints;

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
        StartGame();

        GameLoop();

        EndGame();
    }

    private void StartGame()
    {
        GreetPlayer();
        WelcomeScreen();
        _isPlaying = true;
    }

    private void GreetPlayer()
    {
        Console.WriteLine($"Hello {Player.PlayerId}, and welcome to Rock Paper Scissors!\n");
        Console.WriteLine("Here are some ground rules for the game!\n" +
            " -Type in your choice, rock, paper or scissors.\n" +
            " -To win you have to lead by 3 points.\n" +
            " -Your score is based on how few tries you can beat the computer\n");
        Console.WriteLine("To see high scores press 'h' or press any other key to start a new game!");
        var keyPressed = Console.ReadKey();
        if (keyPressed.Key == ConsoleKey.H)
        {
            _controller.RunHighScoreIO();
        }

        Console.Clear();
    }

    private void WelcomeScreen()
    {
        var welcomeScreen =
@$"
 ___________________________________________
|                                           |
| ***************************************** |
| *                                       * |
| *      W E L C O M E   T O   T H E      * |
| *                                       * |
| *       E P I C  B A T T L E  O F       * |
| *                                       * |
| *          R O C K , P A P E R          * |
| *           S C I S S O R S !           * |
| ***************************************** |
|                                           |
|Prepare yourself for the ultimate showdown!|
|     Only the strongest will prevail...    |
|___________________________________________|
";
        Console.WriteLine(welcomeScreen);
        Console.ReadKey();
        Console.Clear();
    }

    private void GameLoop()
    {
        PlayingFieldScreen("Round 1");
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
        return _choices
            .Any(c => c
            .Equals(input, StringComparison.OrdinalIgnoreCase) ||
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

    private bool HasPlayerWon() => (_playerPoints - _cpuPoints) == WINNING_SCORE_DIFFERENCE;

    private bool HasCPUWon() => (_cpuPoints - _playerPoints) == WINNING_SCORE_DIFFERENCE;

    private bool Quit()
    {
        return PlayerHand.Equals("q", StringComparison.OrdinalIgnoreCase);
    }

    private void DisplayResult()
    {
        Console.Clear();

        switch (Results)
        {
            case -1:
                _cpuPoints++;
                PlayingFieldScreen("Computers point!");
                break;

            case 0:
                PlayingFieldScreen("It's a tie!");
                break;

            case 1:
                _playerPoints++;
                PlayingFieldScreen("Your point!");
                break;

            default:
                break;
        }
    }

    private void PlayingFieldScreen(string? result)
    {
        var playingField =
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
        Console.WriteLine(playingField);
    }

    private void PlayerWinner()
    {
        DisplayWinnerScreen();
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
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
        _controller.Score = totalMoves;
        _controller.CreateAndAddNewHighScore();
    }

    private void DisplayWinnerScreen()
    {
        var winnerScreen =
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
|         Your Score: {totalMoves} points!           |
|                                         |
|   Would you like to add your score to   |
|          the High Score List?           |
|    Press 'Y' for Yes or any other key   |
|              to continue...             |
|_________________________________________|
";

        Console.Clear();
        Console.WriteLine(winnerScreen);
    }

    private void DisplayYouLoseScreen()
    {
        var youLoseScreen =
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
        Console.Clear();
        Console.WriteLine(youLoseScreen);
        Console.ReadKey();
    }

    private void CPUWinner()
    {
        DisplayYouLoseScreen();
        EndGame();
    }

    private void EndGame()
    {
        Console.Clear();
        var goodbyeScreen =
@$"
 ___________________________________________
|                                           |
| ***************************************** |
| *                                       * |
| *       T H A N K   Y O U   F O R       * |
| *                                       * |
| *           P L A Y I N G ! !           * |
| *                                       * |
| ***************************************** |
|                                           |
|               See you soon!               |
|___________________________________________|
";

        Console.WriteLine(goodbyeScreen);
        Console.ReadKey();
    }
}