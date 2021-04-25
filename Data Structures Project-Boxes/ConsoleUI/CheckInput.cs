using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    static public class CheckInput
    {
        public static bool CheckInputInt(string input, out int number)
        {

            if (!int.TryParse(input, out number))
            {
                Console.WriteLine("Invalid input. Please try again");
                return false;
            }
            if (number < 0)
            {
                Console.WriteLine("Number must be above 0. Please try again");
                return false;
            }
            return true;
        }
        public static bool CheckInputDouble(string input, out double number)
        {

            if (!double.TryParse(input, out number))
            {
                Console.WriteLine("Invalid input. Please try again");
                return false;
            }
            if (number < 0)
            {
                Console.WriteLine("Number must be above 0. Please try again");
                return false;
            }
            return true;
        }
    }
}
