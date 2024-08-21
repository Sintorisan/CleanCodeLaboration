using CleanCodeLaboration.Interfaces.GameInterfaces;

namespace CleanCodeLaboration.Interfaces.ServiceInterfaces;

public interface IGameIOBuilder
{
    IGameIO BuildMoo(string playerName);

    IGameIO BuildRockPaperScissors(string playerName);
}