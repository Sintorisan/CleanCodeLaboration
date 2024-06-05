using CleanCodeLaboration.Games.Moo;
using CleanCodeLaboration.Interfaces;

namespace MooGame;

partial class MainClass
{
    public static void Main(string[] args)
    {
        IGameLogic gameLogic = new MooGameLogic();
        ICartridge cartridge = new MooCartridge(gameLogic);
        MooGameEngine consoleGameEngine = new MooGameEngine(cartridge);

        consoleGameEngine.RunConsoleGame();
    }
}