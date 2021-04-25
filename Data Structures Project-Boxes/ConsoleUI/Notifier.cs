using BoxesProject.DataStructures;
using BoxesProject.Interfasces;
using ConsoleUI.Menus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class Notifier : IUiNotifier
    {
        public void OnDeletionFromStock(string message, BoxDetails box)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (box != null)
            {
                Console.WriteLine($"Width: {box.DisplayBoxWidth}, Height: {box.DisplayBoxHeight}, " +
                $"Inventory Amount: {box.DisplayBoxAmount}, Last Accessed on: {box.LastAccessDate}");
            }    
        }
        public bool OnGetUserResponse(List<BoxDetails> foundBoxesList)
        {
            string isRespond = null;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nWe found the following boxes: ");
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < foundBoxesList.Count; i++)
            {
                Console.WriteLine($"{i + 1,3}. Width: {foundBoxesList[i].DisplayBoxWidth,-5} Height: {foundBoxesList[i].DisplayBoxHeight,-5} " +
                $"Available Amount: {foundBoxesList[i].DisplayBoxAmount,-5}");
            }

            Console.WriteLine("\nDo you wish to proceed with the purchase?\n" +
                "Enter 1 to continue or press any key to return");
            while (isRespond == null)
            {
                isRespond = Console.ReadLine();
            }
            return isRespond == "1";
        }
        public void OnMessage(string message, BoxDetails box)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (box != null)
            {
                Console.WriteLine($"Width: {box.DisplayBoxWidth}, Height: {box.DisplayBoxHeight}, " +
                $"Inventory Amount: {box.DisplayBoxAmount}, Last Accessed on: {box.LastAccessDate}");
            }
        }
        public void OnOutOfStockAlert(string message, BoxDetails box)
        {
            string isRespond = null;
            int amount;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (box != null)
            {
                Console.WriteLine($"Width: {box.DisplayBoxWidth}, Height: {box.DisplayBoxHeight}, " +
                $"Inventory Amount: {box.DisplayBoxAmount}, Last Accessed on: {box.LastAccessDate}");
            }           

            Console.WriteLine("\nDo you want to restock?\n"+
                "Enter 1 to continue or press any key to return");

            while (isRespond == null)
            {
                isRespond = Console.ReadLine();
            }
            if(isRespond == "1")
            {
                do
                {
                    Console.Write($"\nHow many unit would you like to order? ");

                } while (!CheckInput.CheckInputInt(Console.ReadLine(), out amount));

                SettingsMenu.Manager.InsertIntoInventory(box.DisplayBoxWidth, box.DisplayBoxHeight, amount);
            }
            return;
        }
        public void OnDisplayAllInventory(List<BoxDetails> boxesList)
        {
            Console.WriteLine("\nInventory List:");
            for (int i = 0; i < boxesList.Count; i++)
            {                
                Console.WriteLine($"{i+1, 3}. Width: {boxesList[i].DisplayBoxWidth, -5} Height: {boxesList[i].DisplayBoxHeight, -5} " +
                $"Inventory Amount: {boxesList[i].DisplayBoxAmount, -5} Last Accessed on: {boxesList[i].LastAccessDate}");
            }
        }
    }
}
