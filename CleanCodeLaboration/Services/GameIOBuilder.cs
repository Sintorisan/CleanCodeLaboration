using CleanCodeLaboration.Games;
using CleanCodeLaboration.Games.Moo;
using CleanCodeLaboration.Games.RockPaperScissors;
using CleanCodeLaboration.Interfaces.GameInterfaces;
using CleanCodeLaboration.Interfaces.ServiceInterfaces;

namespace CleanCodeLaboration.Services;

public class GameIOBuilder : IGameIOBuilder
{
    private IGameIO? _gameIO;
    private IGameplayController? _cartridge;
    private IGameLogic? _gameLogic;

    public IGameIO BuildMoo(string playerName)
    {
        _gameLogic = new MooGameLogic();
        _cartridge = new GameplayController(_gameLogic, playerName);
        _gameIO = new MooGameIO(_cartridge);
        return _gameIO;
    }

    public IGameIO BuildRockPaperScissors(string playerName)
    {
        _gameLogic = new RockPaperScissorsLogic();
        _cartridge = new GameplayController(_gameLogic, playerName);
        _gameIO = new RockPaperScissorsGameIO(_cartridge);
        return _gameIO;
    }
}