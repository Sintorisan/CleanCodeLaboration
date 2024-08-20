using CleanCodeLaboration.Games.Moo;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;

namespace CleanCodeLaboration.Services;
public class GameEngineBuilderService : IGameEngineBuilder
{
    private IGameUI? _gameEngine;
    private ICartridge? _cartridge;
    private IGameLogic? _gameLogic;


    public IGameUI BuildMoo(string playerName)
    {
        _gameLogic = new MooGameLogic();
        _cartridge = new MooCartridge(_gameLogic, playerName);
        _gameEngine = new MooGameUI(_cartridge);
        return _gameEngine;
    }
}
