using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

// Spelets UI.

namespace CleanCodeLaboration.Games.Moo
{
    public class MooGameIO : IGameIO
    {
        private readonly IGameplayController _controller;
        private string _playerCurrentGuess = string.Empty;
        private bool _isPlaying = true;
        private bool _isVictorious = false;

        public IPlayer Player => _controller.GetCurrentPlayer();
        public string NumberCombination => _controller.FirstDataStorage;
        public string PlayerGuessStatus => _controller.SecondDataStorage;

        public MooGameIO(IGameplayController controller)
        {
            _controller = controller;
        }

        public void Run()
        {
            GreetPlayer();
            PrepareGame();

            do
            {
                HandlePlayerGuess();
                DisplayResults();
            } while (_isPlaying);

            Console.Clear();
        }

        private void GreetPlayer()
        {
            Console.WriteLine($"Hello {Player.PlayerId}, and welcome to Moo!\n");
            Console.WriteLine("Here are some ground rules for the game!\n" +
                " - A secret four-digit number combination will be generated, each digit ranging from 0 to 9.\n" +
                " - Your goal is to guess this four-digit number.\n" +
                " - After each guess, you'll receive a hint to help you get closer to the secret number.\n" +
                " - The hint consists of two parts:\n" +
                "     1. 'Bulls': The number of digits that are correct and in the correct position.\n" +
                "     2. 'Cows': The number of digits that are correct but in the wrong position.\n" +
                " - For example, if the secret number is '1234' and you guess '1325':\n" +
                "     - You have 1 Bull (digit '1' is correct and in the correct position).\n" +
                "     - You have 2 Cows (digits '2' and '3' are correct but in the wrong positions).\n" +
                " - Keep guessing until you have all 4 Bulls, which means you've guessed the number correctly!\n"); Console.WriteLine("To see high scores press 'h' or press any other key to start a new game!");
            var keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.H)
            {
                _controller.RunHighScoreIO();
            }

            Console.Clear();
        }

        private void PrepareGame()
        {
            _isPlaying = true;
            _controller.GameStartUp();
        }

        private void HandlePlayerGuess()
        {
            string guess = GetPlayerGuess();

            if (ShouldContinue(guess))
            {
                _playerCurrentGuess = guess;
                SetGuess(guess);
                CompareGuess();
            }
            else
            {
                EndGame();
            }
        }

        private string GetPlayerGuess()
        {
            Console.Write("Type in your guess: ");
            return Console.ReadLine();
        }

        private bool ShouldContinue(string input)
        {
            if (input.ToLower() == "q")
            {
                _isPlaying = false;
                return false;
            }
            return true;
        }

        private void SetGuess(string guess)
        {
            _controller.SecondDataStorage = guess;
        }

        private void CompareGuess()
        {
            _controller.GamePlayLoop();

            if (IsCorrectGuess())
            {
                _isVictorious = true;
                EndGame();
            }
        }

        private bool IsCorrectGuess() => _playerCurrentGuess == NumberCombination;

        private void EndGame()
        {
            if (_isVictorious)
            {
                Console.WriteLine("Congrats, you found the hidden number!");
                Console.WriteLine(_playerCurrentGuess);
                AddHighScore();
            }
            else
            {
                Console.WriteLine("Thank you for playing");
            }

            _controller.GameShutDown();
            _isPlaying = false;
        }

        private void AddHighScore()
        {
            Console.WriteLine("Do you want to add this score to your high score list? y/n");

            if (Console.ReadLine().ToLower() == "y")
            {
                _controller.CreateAndAddNewHighScore();
                Console.WriteLine("Highscore added!");
            }
        }

        private void DisplayResults()
        {
            if (_isPlaying)
            {
                Console.WriteLine($"Your guess: {_playerCurrentGuess}");
                Console.WriteLine($"Bulls n' Cows: {PlayerGuessStatus}");
            }
        }
    }
}