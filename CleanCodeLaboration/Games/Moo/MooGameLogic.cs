using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.Moo;

public class MooGameLogic : IGameLogic
{
    private const int MAX_NUMBER = 9999;
    private const int MIN_NUMBER = 1000;
    private Random randomGenerator = new Random();

    public string GameId { get; } = "Cows N Bulls";
    public int Score { get; set; }
    public string FirstDataStorage { get; set; } = string.Empty; //Random number to guess
    public string SecondDataStorage { get; set; } = string.Empty; //User guess


    public void GameStartUp()
    {
        CreateNumberCombination();
    }

    public void GamePlayLoop()
    {
        CompareGuessToNumber();
    }

    public void GameShutDown()
    {
        ResetStorage();
    }


    private void CreateNumberCombination()
    {// Ska slumpa fram 4 unika siffror
        int numberCombination = randomGenerator.Next(MIN_NUMBER, MAX_NUMBER);
        FirstDataStorage = numberCombination.ToString();
    }

    private void CompareGuessToNumber()
    {
        Score++;

        var result = GetCowsAndBullsFromGuess();

        SecondDataStorage = result;
    }

    private string GetCowsAndBullsFromGuess()
    {
        int cows = 0, bulls = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (FirstDataStorage[i] == SecondDataStorage[j])
                {
                    if (i == j)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }

        return CowAndBullsResponse(cows, bulls);

    }
    string CowAndBullsResponse(int cows, int bulls) => "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);

    public void ResetStorage()
    {
        FirstDataStorage = string.Empty;
        SecondDataStorage = string.Empty;
    }
}
