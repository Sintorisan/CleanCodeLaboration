using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.Moo
{
    public class MooGameIO : IGameIO
    {
        private const string INVALID_CHOICE_MESSAGE = "Invalid input. Please try again.";

        private readonly IGameplayController _controller;
        private string _playerCurrentGuess = string.Empty;
        private bool _isPlaying = true;

        public IPlayer Player => _controller.GetCurrentPlayer();
        public string NumberCombination => _controller.FirstDataStorage;
        public string PlayerGuessStatus => _controller.SecondDataStorage;

        public MooGameIO(IGameplayController controller)
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
            PrepareGame();
            do
            {
                Console.WriteLine(NumberCombination);
                GetPlayerGuess();
                HandleResults();
            } while (_isPlaying);
        }

        private void PrepareGame()
        {
            _isPlaying = true;
            _controller.GameStartUp();
            PrintScreen(ResultScreen(), false);
        }

        private void GetPlayerGuess()
        {
            var playerInput = GetValidUserInput();
            _playerCurrentGuess = playerInput;
        }

        private string GetValidUserInput()
        {
            var input = Console.ReadLine();

            while (!ApprovedChoice(input!))
            {
                Console.WriteLine(INVALID_CHOICE_MESSAGE);
                input = Console.ReadLine();
            }

            return input!;
        }

        private bool ApprovedChoice(string input)
        {
            var isApproved = input.Length == 4 || input.Equals("q", StringComparison.OrdinalIgnoreCase);

            return isApproved;
        }

        private void HandleResults()
        {
            if (Quit())
            {
                _isPlaying = false;
                return;
            }

            CompareGuess();
            PrintScreen(ResultScreen(), false);

            if (IsCorrectGuess())
            {
                PlayerWinner();
            }
        }

        private bool Quit() => _playerCurrentGuess.Equals("q", StringComparison.OrdinalIgnoreCase);

        private void CompareGuess()
        {
            //Sets the player guess as the SecondDataStorage which is then processed in the game logic
            //and returns the cow and bulls
            _controller.SecondDataStorage = _playerCurrentGuess;

            _controller.GamePlayLoop();
        }

        private bool IsCorrectGuess() => _playerCurrentGuess == NumberCombination;

        private void PlayerWinner()
        {
            _isPlaying = false;
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
            _controller.CreateAndAddNewHighScore();
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
|*           G A M E   O F               *|
|*                                       *|
|*                M O O !                *|
|*****************************************|
|                                         |
| {Player.PlayerId,22}!                 |
| Welcome to the ultimate guessing game!  |
|                                         |
| Here are the rules:                     |
| - Guess the secret 4-digit number.      |
| - Each digit is between 0 and 9.        |
| - Hints after each guess:               |
|     Bulls: Correct digit, right place.  |
|     Cows: Correct digit, wrong place.   |
| - Example: Secret '1234', guess '1325': |
|     1 Bull ('1') and 2 Cows ('2' & '3').|
| - Keep guessing until you get 4 Bulls!  |
|                                         |
| To see high scores press 'h', or press  |
| any other key to start a new game!      |
|_________________________________________|
";
        }

        private string ResultScreen()
        {
            return @$"
___________________________________________
|                                         |
|*****************************************|
|*                                       *|
|*           R E S U L T S               *|
|*                                       *|
|*****************************************|
|                                         |
| Your Guess: {_playerCurrentGuess,26}  |
|                                         |
| Bulls & Cows: {PlayerGuessStatus,24}  |
|                                         |
|*****************************************|
|_________________________________________|

";
        }

        private string WinnerScreen()
        {
            return @$"
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
|         Your Score: {_controller.Score} points!           |
|                                         |
|   Would you like to add your score to   |
|          the High Score List?           |
|    Press 'Y' for Yes or any other key   |
|              to continue...             |
|_________________________________________|
";
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
    }
}