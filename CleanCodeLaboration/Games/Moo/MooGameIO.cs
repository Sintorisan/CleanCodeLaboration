using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

// Spelets UI.

namespace CleanCodeLaboration.Games.Moo
{
    public class MooGameIO : IGameIO
    {
        private readonly IGameplayController _cartridge;
        private string _playerCurrentGuess = string.Empty;
        private bool _isPlaying = true;
        private bool _isVictorious = false;

        public IPlayer Player => _cartridge.GetCurrentPlayer();
        public string NumberCombination => _cartridge.GetFirstDataStorage;
        public string PlayerGuessStatus => _cartridge.GetSecondDataStorage;

        public MooGameIO(IGameplayController cartridge)
        {
            _cartridge = cartridge;
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
            Console.WriteLine($"Hello {Player.PlayerId}, and welcome to Cows n' Bulls!\n");

            Console.WriteLine("Want to see your high scores? y/n");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayPlayerHighScores();
            }

            Console.Clear();
        }

        private void DisplayPlayerHighScores()
        {
            var highScores = _cartridge.GetAllPlayerHighScores(Player.PlayerId);

            if (!highScores.Any())
            {
                Console.WriteLine("You currently don't have any high scores!");
            }
            else
            {
                foreach (var highScore in highScores)
                {
                    Console.WriteLine($"Score: {highScore.HighScore} Date: {highScore.Date.ToShortDateString()}");
                }
            }

            Console.WriteLine("Press any key to start a new game!");
            Console.ReadKey();
        }

        private void PrepareGame()
        {
            _isPlaying = true;
            _cartridge.GameStartUp();
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
            _cartridge.SetSecondDataStorage(guess);
        }

        private void CompareGuess()
        {
            _cartridge.GamePlayLoop();

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

            _cartridge.GameShutDown();
            _isPlaying = false;
        }

        private void AddHighScore()
        {
            Console.WriteLine("Do you want to add this score to your high score list? y/n");

            if (Console.ReadLine().ToLower() == "y")
            {
                _cartridge.CreateNewHighScore();
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