using CleanCodeLaboration.Db;
using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.DbInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;

namespace CleanCodeLaboration.Services;

public class PlayerService : IPlayerService
{
    private readonly IDatabase<IPlayer> _database;
    public IPlayer? Player { get; set; }

    public PlayerService(string playerName)
    {
        _database = new PlayerDb();
        InitialLoad(playerName);
    }

    public void InitialLoad(string playerName)
    {
        Player = SetPlayer(playerName);
    }

    public IPlayer GetCurrentPlayer() => Player!;

    public IPlayer SetPlayer(string playerName)
    {
        if (isPlayerFound(playerName))
        {
            return GetSinglePlayer(playerName);
        }

        return CreatePlayer(playerName);
    }

    public bool isPlayerFound(string playerId) => _database.GetAll().Any(p => p.PlayerId.ToLower() == playerId.ToLower());

    public IPlayer GetSinglePlayer(string userName)
    {
        var player = _database.GetAll().FirstOrDefault(p => p.PlayerId == userName)!;

        return player;
    }

    private IPlayer CreatePlayer(string playerName)
    {
        var newPlayer = new Player { PlayerId = playerName };

        if (_database.Add(newPlayer))
        {
            return newPlayer;
        }
        return new Player { PlayerId = "DefaultPlayer" };
    }
}