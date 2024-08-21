using CleanCodeLaboration;

namespace MooGame;

internal partial class MainClass
{
    public static void Main(string[] args)
    {
        GameConsole _gameConsole = new GameConsole();

        _gameConsole.Run();
    }
}