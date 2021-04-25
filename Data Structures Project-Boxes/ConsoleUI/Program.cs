using BoxesProject;
using ConsoleUI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static bool _isFirstRun = true;
        static MainMenu _mainMenu;
        static public bool IsFirstRun { get { return _isFirstRun; } set { _isFirstRun = value; } }
        static public MainMenu MainMenu { get { return _mainMenu; } }

        static void Main(string[] args)
        {
            _isFirstRun = true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nHELLO!\nWelcome to the best box-warehouse management system ever!\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            _mainMenu = new MainMenu();
            _mainMenu.Show();

            Console.ReadLine();
        }

        
    }
}
