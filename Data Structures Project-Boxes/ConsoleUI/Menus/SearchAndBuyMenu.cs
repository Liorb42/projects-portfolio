using ConsoleUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Menus
{
    public class SearchAndBuyMenu : IMenu
    {
        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================================");
            Console.WriteLine("SEARCH NEMU\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            FindAndBuy();
        }
        public void FindAndBuy()
        {
            double width;
            double height;
            int amount;
            int splits;

            do
            {
                Console.Write($"Please enter desired width:  ");

            } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out width));
            do
            {
                Console.Write($"Please enter desired height:  ");

            } while (!CheckInput.CheckInputDouble(Console.ReadLine(), out height));
            do
            {
                Console.Write($"Please enter desired amount:  ");

            } while (!CheckInput.CheckInputInt(Console.ReadLine(), out amount));
            do
            {
                Console.Write($"Please enter allowed splits number:  ");

            } while (!CheckInput.CheckInputInt(Console.ReadLine(), out splits));

            SettingsMenu.Manager.Buy(width, height, amount, splits);
            Program.MainMenu.Show();
        }
        

    }
}
