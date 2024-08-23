using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.RockPaperScissors
{
    public class RockPaperScissorsLogic : IGameLogic
    {
        private readonly string[] _choices = { "Rock", "Paper", "Scissors" };
        private readonly Random _random = new Random();

        public string GameId { get; } = "Rock Paper Scissors";
        public int Score { get; set; }
        public string FirstDataStorage { get; set; } = string.Empty; // Computer choice
        public string SecondDataStorage { get; set; } = string.Empty; // Player choice

        public void GameStartUp()
        {
            SetComputerChoice();
        }

        public void GamePlayLoop()
        {
            DetermineWinner();
        }

        public void GameShutDown()
        {
            ResetStorage();
        }

        private void SetComputerChoice()
        {
            FirstDataStorage = _choices[_random.Next(_choices.Length)];
        }

        private void DetermineWinner()
        {
            if (SecondDataStorage.Equals(FirstDataStorage, StringComparison.OrdinalIgnoreCase))
            {
                // It's a tie
                Score = 0;
            }
            else if (IsPlayerWinner())
            {
                // Player wins
                Score = 1;
            }
            else
            {
                // Player loses
                Score = -1;
            }
        }

        private bool IsPlayerWinner()
        {
            return (SecondDataStorage.Equals("Rock", StringComparison.OrdinalIgnoreCase) && FirstDataStorage.Equals("Scissors", StringComparison.OrdinalIgnoreCase))
                || (SecondDataStorage.Equals("Paper", StringComparison.OrdinalIgnoreCase) && FirstDataStorage.Equals("Rock", StringComparison.OrdinalIgnoreCase))
                || (SecondDataStorage.Equals("Scissors", StringComparison.OrdinalIgnoreCase) && FirstDataStorage.Equals("Paper", StringComparison.OrdinalIgnoreCase));
        }

        private void ResetStorage()
        {
            FirstDataStorage = string.Empty;
            SecondDataStorage = string.Empty;
            Score = 0;
        }
    }
}