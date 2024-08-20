using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Interfaces.ServiceInterfaces;

public interface IGameEngineBuilder
{
    IGameUI BuildMoo(string playerName);
}
