using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Games.TicTacToe;

public class TicTacToeCartridge : ICartridge
{
    public string GetFirstDataStorage => throw new NotImplementedException();

    public string GetSecondDataStorage => throw new NotImplementedException();

    public void CreateNewHighScore()
    {
        throw new NotImplementedException();
    }

    public void GamePlayLoop()
    {
        throw new NotImplementedException();
    }

    public void GameShutDown()
    {
        throw new NotImplementedException();
    }

    public void GameStartUp()
    {
        throw new NotImplementedException();
    }

    public List<HighScoreForm> GetAllHighScores()
    {
        throw new NotImplementedException();
    }

    public List<HighScoreForm> GetAllPlayerHighScores(string playerName)
    {
        throw new NotImplementedException();
    }

    public IPlayer GetCurrentPlayer()
    {
        throw new NotImplementedException();
    }

    public int GetCurrentScore()
    {
        throw new NotImplementedException();
    }

    public HighScoreForm GetHighestScore(string playerName)
    {
        throw new NotImplementedException();
    }

    public IPlayer GetSingleIPlayer(string playerName)
    {
        throw new NotImplementedException();
    }

    public bool isExistingPlayer(string playerName)
    {
        throw new NotImplementedException();
    }

    public void SetFirstDataStorage(string data)
    {
        throw new NotImplementedException();
    }

    public void SetSecondDataStorage(string data)
    {
        throw new NotImplementedException();
    }
}
