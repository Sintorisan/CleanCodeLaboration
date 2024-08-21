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
            if (FirstDataStorage == SecondDataStorage)
            {
                // It's a tie
                Score = 0;
            }
            else if ((SecondDataStorage == "Rock" && FirstDataStorage == "Scissors") ||
                     (SecondDataStorage == "Paper" && FirstDataStorage == "Rock") ||
                     (SecondDataStorage == "Scissors" && FirstDataStorage == "Paper"))
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

        private void ResetStorage()
        {
            FirstDataStorage = string.Empty;
            SecondDataStorage = string.Empty;
        }
    }
}
