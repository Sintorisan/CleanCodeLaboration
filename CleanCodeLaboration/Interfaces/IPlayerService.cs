namespace CleanCodeLaboration.Interfaces;

public interface IPlayerService
{
    IPlayer GetSinglePlayer(string playerName);
    bool isPlayerFound(string playerId);
    public void InitialLoad(string userName);


}
