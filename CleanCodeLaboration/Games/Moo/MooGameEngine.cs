using CleanCodeLaboration.Interfaces;

namespace CleanCodeLaboration.Games.Moo
{
    public class MooGameEngine
    {
        private readonly ICartridge _cartridge;
        private string _playerCurrentGuess = string.Empty;
        private bool _isPlaying = true;

        public IPlayer Player { get; set; }
        public string NumberCombination => _cartridge.GetFirstDataStorage;
        public string PlayerGuessStatus => _cartridge.GetSecondDataStorage;

        public MooGameEngine(ICartridge cartridge)
        {
            _cartridge = cartridge;
        }


        public void RunConsoleGame()
        {
            GreetPlayer();

            PrepareGame();

            Console.WriteLine(NumberCombination);
            while (_isPlaying)
            {
                HandlePlayerGuess();

                DisplayResults();
            }

            CleanUp();
        }

        private void GreetPlayer()
        {
            Console.WriteLine("Hi and welcome to Cows n' Bulls!\n");
            Console.WriteLine("Enter your user name:\n");

            string playerName = Console.ReadLine();
            bool isExistingPlayer = StartPlayerService(playerName);

            string greeting = isExistingPlayer ? $"Welcome back {Player.PlayerId}" : $"Welcome {Player.PlayerId}";
            Console.WriteLine(greeting);
        }

        private void PrepareGame()
        {
            _isPlaying = true;
            _cartridge.GameStartUp();
        }

        private string GetPlayerGuess()
        {
            Console.Write("Type in your guess: ");
            string guess = Console.ReadLine();
            return guess;
        }

        private bool StartPlayerService(string playerName)
        {
            bool isExistingPlayer = _cartridge.isExistingPlayer(playerName);

            _cartridge.StartPlayerService(playerName);
            Player = _cartridge.GetSingleIPlayer(playerName);

            return isExistingPlayer;
        }

        private void HandlePlayerGuess()
        {
            string guess = GetPlayerGuess();

            if (!WantToQuit(guess))
            {
                _playerCurrentGuess = guess;

                SetGuess(guess);
                CompareGuess();
            }
        }

        private bool WantToQuit(string input)
        {
            if (input.ToLower() == "q")
            {
                _isPlaying = false;
                return true;
            }

            return false;
        }

        private void SetGuess(string guess)
        {
            _cartridge.SetSecondDataStorage(guess);
        }

        private bool isSameNumber() => _playerCurrentGuess == NumberCombination;

        private void CompareGuess()
        {
            _cartridge.GamePlayLoop();

            if (isSameNumber())
            {
                _cartridge.GameShutDown();
                _isPlaying = false;
            }
        }

        private void DisplayResults()
        {
            if (_isPlaying)
            {
                Console.WriteLine($"Your guess: {_playerCurrentGuess}");
                Console.WriteLine($"Bulls n' Cows: {PlayerGuessStatus}");
            }
            else
            {
                Console.WriteLine("Congrats you found the hidden number");
                Console.WriteLine(_playerCurrentGuess);
            }
        }

        private void CleanUp()
        {

        }
    }
}
