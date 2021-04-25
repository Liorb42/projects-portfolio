using ConsoleUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Menus
{
    public class ManageInventoryMenu : IMenu
    {
        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================================");
            Console.WriteLine("INVENTORY MENU\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Please select an option from the list: \n" +
                "1. Print all inventory details\n" +
                "2. Print information for a specific box\n" +
                "3. Order additional boxes\n" +
                "4. Back to main menu\n");

            InventoryFlow(Console.ReadLine());

            Console.ReadLine();
        }
        private void InventoryFlow(string input)
        {            
            switch (input)
            {
                case "1":
                    PrintInventory();
                    break;
                case "2":
                    PrintBoxInfo();
                    break;
                case "3":
                    AddBox();
                    break;
                case "4":
                    Program.MainMenu.Show();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again");
                    InventoryFlow(Console.ReadLine());
                    break;
            }
        }
        private void PrintInventory()
        {
            SettingsMenu.Manager.DisplayAllInventory();
            Show();
        }
        private void PrintBoxInfo()
        {
            double width;
            double height;

            do
            {
                Console.Write($"\nPlease enter desired width to print:  ");

            } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out width));
            do
            {
                Console.Write($"Please enter desired height to print:  ");

            } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out height));

            SettingsMenu.Manager.DisplayBoxDetails(width, height);
            Show();
        }
        private void AddBox()
        {
            double width;
            double height;
            int amount;

            do
            {
                Console.Write($"\nPlease enter desired width to add:  ");

            } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out width));
            do
            {
                Console.Write($"Please enter desired height to add:  ");

            } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out height));

            do
            {
                Console.Write($"How many unit would you like to order? ");

            } while (!CheckInput.CheckInputInt(Console.ReadLine(), out amount));

            SettingsMenu.Manager.InsertIntoInventory(width, height, amount);
            Show();
        }
        
    }
}
