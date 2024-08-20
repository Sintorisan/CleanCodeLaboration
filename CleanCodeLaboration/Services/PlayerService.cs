﻿using CleanCodeLaboration.Entities;
using CleanCodeLaboration.Interfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;

namespace CleanCodeLaboration.Services;

public class PlayerService : IPlayerService
{
    private const string FILE_PATH = "playersDb.csv";
    public List<IPlayer> _allPlayers = new List<IPlayer>();
    public IPlayer? Player { get; set; }

    public PlayerService(string playerName)
    {
        InitialLoad(playerName);
    }

    public void InitialLoad(string playerName)
    {
        Player = SetPlayer(playerName);
        GetAllPlayers();
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
    public bool isPlayerFound(string playerId)
    {
        if (!File.Exists(FILE_PATH))
            return false;

        return _allPlayers.Any(p => p.PlayerId.ToLower() == playerId.ToLower());
    }
    public IPlayer GetSinglePlayer(string userName)
    {
        return _allPlayers.FirstOrDefault(p => p.PlayerId == userName)!;
    }

    private IPlayer CreatePlayer(string playerName)
    {
        var newPlayer = new Player { PlayerId = playerName };

        File.AppendAllText(FILE_PATH, $"{newPlayer.PlayerId}\n");

        return newPlayer;
    }

    //Ska vara en låtsas db.
    //Bryter ner en .csv fil för att fylla på spelarens datatyp och returnerar en lista.
    public void GetAllPlayers()
    {
        if (File.Exists(FILE_PATH))
        {
            var playerTable = File.ReadAllLines(FILE_PATH);

            foreach (string player in playerTable)
            {
                var data = player.Split(',');
                var playerToAdd = new Player
                {
                    PlayerId = data[0],
                };
                _allPlayers.Add(playerToAdd);
            }
        }
    }



}

