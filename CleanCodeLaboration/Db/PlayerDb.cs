using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.DbInterfaces;

namespace CleanCodeLaboration.Db;

public class PlayerDb : IDatabase<IPlayer>
{
    private readonly List<IPlayer> _players = new List<IPlayer>();
    private const string FILE_PATH = "playersDb.csv";

    public PlayerDb()
    {
        InitialLoad();
    }

    public void InitialLoad()
    {
        try
        {
            if (File.Exists(FILE_PATH))
            {
                var playerTable = File.ReadAllLines(FILE_PATH);

                foreach (string player in playerTable)
                {
                    var data = player.Split(',');
                    _players.Add(new Player
                    {
                        PlayerId = data[0],
                    });
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to initialize the player database.", ex);
        }
    }

    public void Add(IPlayer newPlayer)
    {
        try
        {
            File.AppendAllText(FILE_PATH, $"{newPlayer.PlayerId}\n");
            _players.Add(newPlayer);
        }
        catch (Exception ex)
        {
            throw new Exception("Player could not be added.", ex);
        }
    }

    public List<IPlayer> GetAll() => _players;
}