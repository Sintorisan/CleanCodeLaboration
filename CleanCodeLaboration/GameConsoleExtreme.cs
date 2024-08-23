//using CleanCodeLaboration.Entities;
//using CleanCodeLaboration.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CleanCodeLaboration;

//public class GameConsoleExtreme : IGameConsole
//{
//    public void Run()
//    {
//        throw new NotImplementedException();
//    }

//    public void RunHighScoreIO()
//    {
//        string[] options = { "All High Scores", "Your High Scores", "Exit" };
//        int selectedIndex = 0;

//        while (true)
//        {
//            DisplayMenu(options, selectedIndex);

//            var key = Console.ReadKey(true).Key;

//            switch (key)
//            {
//                case ConsoleKey.UpArrow:
//                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
//                    break;

//                case ConsoleKey.DownArrow:
//                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
//                    break;

//                case ConsoleKey.Enter:
//                    ExecuteOption(selectedIndex);
//                    if (selectedIndex == options.Length - 1) return; // Exit if "Exit" is chosen
//                    break;
//            }
//        }
//    }

//    private static void DisplayMenu(string[] options, int selectedIndex)
//    {
//        Console.Clear();
//        for (int i = 0; i < options.Length; i++)
//        {
//            if (i == selectedIndex)
//            {
//                Console.Write("=> "); // Arrow pointing to the selected option
//            }
//            else
//            {
//                Console.Write("   ");
//            }

//            Console.WriteLine(options[i]);
//        }

//        // Simulate blinking by clearing and redrawing the arrow
//        Thread.Sleep(300);
//    }

//    private void ExecuteOption(int selectedIndex)
//    {
//        Console.Clear();

//        switch (selectedIndex)
//        {
//            case 0:
//                DisplayAllHighScores();
//                break;

//            case 1:
//                DisplayAllIndividualHighScores(_player);
//                break;

//            case 2:
//                return;
//        }

//        Console.WriteLine("\nPress any key to return to the menu...");
//        Console.ReadKey();
//    }
//}