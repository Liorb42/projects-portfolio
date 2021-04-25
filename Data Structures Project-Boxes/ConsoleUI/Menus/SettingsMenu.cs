using BoxesProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI.Interfaces;

namespace ConsoleUI.Menus
{
    public class SettingsMenu : IMenu
    {
        private static Manager _manager;
        private static Notifier _notifier;
        private static ConfigurationParams _configParams;
        private bool _isAdminSet;
        private string _userName;
        private string _password;
        private int _maxCapacityPerBox;
        private double _searchDivMargin;
        private int _alertUnitLimit;
        private int _expirationInterval;
        private string _deleteTimeOfDay;

        public SettingsMenu()
        {
            _notifier = new Notifier();
            _isAdminSet = false;
        }
        public static Manager Manager { get => _manager; }
        public static Notifier Notifier { get => _notifier; }

        public void Show()
        {          
            if (!_isAdminSet)
            {                
                SetAdministrator();
                FirstConfigSystem();
                InsertInventoy();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n================================");
                Console.WriteLine("SETTINGS MENU\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press 1 to change your settings or press any key to return to MAIN MENU");
                SettingsFlow(Console.ReadLine());
            }                     
        }
        private void SettingsFlow(string input)
        {
            switch (input)
            {
                case "1":
                    ChangeConfigurations();
                    break;
                default:
                    Program.MainMenu.Show();
                    break;
            }
        }
        private void SetAdministrator()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================================");
            Console.WriteLine("SETTINGS ADMINISTRATOR\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("\nPlease choose a user name: ");
            _userName = Console.ReadLine();
            _password = GetPassword();
            _isAdminSet = true;
        }
        private string GetPassword()
        {
            Console.WriteLine("\nPlease enter password: ");
            StringBuilder password = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    if (password.Length >= 6)
                        break;
                    else
                    {
                        Console.WriteLine("Password must be at least 6 characters long");
                        Console.WriteLine("Please enter a new password: ");
                        password = new StringBuilder();
                    }
                }
                else if (i.KeyChar != '\u0000') // KeyChar == '\u0000' - a non printable character, e.g. F1, Pause-Break, etc
                {
                    password.Append(i.KeyChar);
                    Console.Write("*");
                }
            }
            return password.ToString();
        }
        private void FirstConfigSystem()
        {
            if (CheckUserAndPassword())
            {
                Console.WriteLine("\nPlease enter parameters for your warehouse: ");
                do
                {
                    Console.Write($"Max units capacity (per box): ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out _maxCapacityPerBox));
                do
                {
                    Console.Write($"Upper diviation margin for search: ");

                } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out _searchDivMargin));
                do
                {
                    Console.Write($"Alert when unit amount reaches: ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out _alertUnitLimit));
                do
                {
                    Console.Write($"Delete unsold items after specified amount of days: ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out _expirationInterval));
                do
                {
                    Console.Write($"Scan hour for unsold items (HH:MM, 24 hour clock): ");

                } while (!CheckHourString(Console.ReadLine(), out _deleteTimeOfDay));

                _configParams = new ConfigurationParams(_maxCapacityPerBox, _searchDivMargin, _alertUnitLimit,
                    _expirationInterval, _deleteTimeOfDay);

                _manager = new Manager(uiNotifier: _notifier, _configParams);

                Console.WriteLine("\nParams set");
            }
        } 
        private void ChangeConfigurations()
        {
            if (CheckUserAndPassword())
            {
                Console.WriteLine("\nThe current parameters for your warehouse are: ");                
                do
                {
                    Console.Write($"Max units capacity (per box) is {_maxCapacityPerBox}. Your new setting is: ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out _maxCapacityPerBox));
                do
                {
                    Console.Write($"Upper diviation margin for search is {_searchDivMargin}. Your new setting is: ");

                } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out _searchDivMargin));
                do
                {
                    Console.Write($"Alert when unit amount reaches {_alertUnitLimit}. Your new setting is: ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out _alertUnitLimit));
                do
                {
                    Console.Write($"Delete unsold items after {_expirationInterval} days. Your new setting is: ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out _expirationInterval));
                do
                {
                    Console.Write($"Scan hour for unsold items {_deleteTimeOfDay}. Your new setting is(HH:MM): ");

                } while (!CheckHourString(Console.ReadLine(), out _deleteTimeOfDay));

                _configParams = new ConfigurationParams(_maxCapacityPerBox, _searchDivMargin, _alertUnitLimit,
                    _expirationInterval, _deleteTimeOfDay);

                _manager.UpdateConfiguration(_configParams);
            }
        }
        private bool CheckHourString(string input, out string hour)
        {
            hour = string.Empty;
            string[] hourParts = input.Split(':');
            if (hourParts.Length == 2)
            {
                if (int.TryParse(hourParts[0], out int partHour) && partHour >= 0 && partHour <= 23)
                    if (int.TryParse(hourParts[1], out int partMinutes) && partMinutes >= 0 && partMinutes <= 59)
                    {
                        hour = partHour.ToString() + ":" + partMinutes.ToString();
                        return true;
                    } 
            }
            return false;
        }
        private bool CheckUserAndPassword()
        {
            string checkPassword;

            do
            {
                Console.WriteLine("\nTo Log into system please enter your user name: ");
            } while (Console.ReadLine() != _userName);

            do
            {
                checkPassword = GetPassword();

            } while (checkPassword != _password);

            return true;
        }
        private void InsertInventoy() 
        {
            Console.WriteLine("\nloading inventory");
            Manager.InsertIntoInventory(10, 11, 20);
            Manager.InsertIntoInventory(10, 12, 20);
            Manager.InsertIntoInventory(10, 12.5, 5);
            Manager.InsertIntoInventory(10.5, 13.5, 5);
            Manager.InsertIntoInventory(10.5, 14.5, 5);
            Manager.InsertIntoInventory(11, 8, 10);
            Manager.InsertIntoInventory(11, 9.5, 10);
            Manager.InsertIntoInventory(15, 3, 5);
            Manager.InsertIntoInventory(15, 11.5, 5);
            Manager.InsertIntoInventory(15, 15, 5);
        }
    }
}
