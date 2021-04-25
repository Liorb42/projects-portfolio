using ConsoleUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Menus
{
    public class MainMenu : IMenu
    {
        
        static SettingsMenu _settings = new SettingsMenu();
        static public SettingsMenu Settings { get { return _settings; }}

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================================");
            Console.WriteLine("MAIN MENU");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (Program.IsFirstRun)
            {
                Program.IsFirstRun = false;
                _settings.Show();                
            }
            PrintMenu();
        }
        private void MainFlow(string input)
        {
            switch (input)
            {
                case "1": //settings
                    _settings.Show();
                    break;
                case "2": // search & buy
                    SearchAndBuyMenu searchBuy = new SearchAndBuyMenu();
                    searchBuy.Show();
                    break;                
                case "3": // Manage inventory
                    ManageInventoryMenu manageInventory = new ManageInventoryMenu();
                    manageInventory.Show();
                    break;
                case "4": // Exit
                    Exit();
                    break;
                default:
                    Console.WriteLine("invalid input please try again");
                    PrintMenu();
                    break;
            }
        }
        private void PrintMenu()
        {
            Console.WriteLine("\nPlease select a menu item:" +
                "\n1. Settings" +
                "\n2. Search & Buy" +
                "\n3. Manage Inventoy" +
                "\n4. Exit\n");
            string input = Console.ReadLine();
            MainFlow(input);
        }
        public void Exit()
        {
            SettingsMenu.Manager.Exit();
            Environment.Exit(0);
        }
    }
}

