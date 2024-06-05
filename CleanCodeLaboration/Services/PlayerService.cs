using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;

namespace CleanCodeLaboration.Services;

public class PlayerService : IPlayerService
{
    private const string FILE_PATH = "playersDb.csv";
    IPlayer? Player { get; set; }

    public void InitialLoad(string playerName)
    {
        Player = SetPlayer(playerName);
    }

    private IPlayer SetPlayer(string playerName)
    {
        if (isPlayerFound(playerName))
        {
            return GetSinglePlayer(playerName);
        }

        return CreatePlayer(playerName);
    }

    //
    private IPlayer CreatePlayer(string playerName)
    {
        var newPlayer = new Player { PlayerId = playerName };

        File.AppendAllText(FILE_PATH, $"{newPlayer.PlayerId}\n");

        return newPlayer;
    }

    public bool isPlayerFound(string playerId)
    {
        return GetAllPlayers().Any(p => p.PlayerId.ToLower() == playerId.ToLower());
    }

    public IPlayer GetSinglePlayer(string userName)
    {
        return GetAllPlayers().FirstOrDefault(p => p.PlayerId == userName)!;
    }

    //Ska vara en låtsas db.
    //Bryter ner en .csv fil för att fylla på spelarens datatyp och returnerar en lista.
    private IList<IPlayer> GetAllPlayers()
    {
        var allUsers = new List<IPlayer>();
        var playerTable = File.ReadAllLines(FILE_PATH);

        foreach (string player in playerTable)
        {
            var data = player.Split(',');
            var playerToAdd = new Player
            {
                PlayerId = data[0],
            };
            allUsers.Add(playerToAdd);
        }

        return allUsers;
    }
}

