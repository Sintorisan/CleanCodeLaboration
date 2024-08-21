namespace CleanCodeLaboration.Interfaces.ServiceInterfaces;

public interface IPlayerService
{
    IPlayer GetCurrentPlayer();

    IPlayer GetSinglePlayer(string playerName);

    bool isPlayerFound(string playerId);

    public void InitialLoad(string userName);
}