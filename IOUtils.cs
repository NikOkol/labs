using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CSharp_proj
{
    static class IOUtils
    {
        public static int Int_input()
        {
            while (true)
            {
                int digit;
                if (!int.TryParse(Console.ReadLine(), out digit))
                {
                    Console.WriteLine("Incorrect value");
                }
                else
                {
                    return digit;
                }
            }
        }

        public static double Double_input()
        {
            while (true)
            {
                double digit;
                if (!double.TryParse(Console.ReadLine(), out digit))
                {
                    Console.WriteLine("Incorrect value");
                }
                else
                {
                    return digit;
                }
            }
        }
        
        public static DateTime Date_input()
        {
            while (true)
            {
                string sValue = Console.ReadLine();
                try
                {
                    DateTime date = DateTime.ParseExact(sValue, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                    return date;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid value!");
                }
            }
        }


        public static double ReadNotZeroValue()
        {
            while (true)
            {
                double n = Double_input();
                if (n == 0)
                {
                    Console.WriteLine("Value can't be zero. Try again...");
                }
                else
                {
                    return n;
                }
            }
        }


        public static double ReadNotLessZeroValue()
        {
            while (true)
            {
                double n = Double_input();
                if (n < 0)
                {
                    Console.WriteLine("Value can't be less than zero. Try again...");
                }
                else
                {
                    return n;
                }
            }
        }

        public static void EditedStringsCompare(string str1, string str2)
        {

            str1 = str1.Trim();
            str2 = str2.Trim();
            str1 = Regex.Replace(str1, @"\s+", " ");
            str2 = Regex.Replace(str2, @"\s+", " ");
            str1 = str1.ToLower();
            str2 = str2.ToLower();
            if (String.Compare(str1, str2) == 0)
            {
                Console.WriteLine("(+)Strings are equal after editing.");
            }
            else
            {
                Console.WriteLine("(-)Strings are not equal after editing.");
            }
        }

        public static void StringReverseCompare(string str1, string str2)
        {
            char[] astr = str1.ToCharArray();
            Array.Reverse(astr);
            str1 = new string(astr);
            if (String.Compare(str1, str2) == 0)
            {
                Console.WriteLine("(+)One line is other in reverse.");
            }
            else
            {
                Console.WriteLine("(-)One line is not other in reverse.");
            }
        }

        public static bool MatchesSearch(string str)
        {
            bool result = false;
            string pattern = @"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+";
            foreach (Match match in Regex.Matches(str, pattern))
            {
                Console.WriteLine("(+){0} email has been found.", match.Value, match.Index);
                if (match.Value != "")
                {
                    result = true;
                }

            }
            pattern = @"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";
            foreach (Match match in Regex.Matches(str, pattern))
            {
                Console.WriteLine("(+){0} IP has been found.", match.Value, match.Index);
                if (match.Value != "")
                {
                    result = true;
                }
            }
            pattern = @"[^ \+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}";
            foreach (Match match in Regex.Matches(str, pattern))
            {
                Console.WriteLine("(+){0} phone number has been found.", match.Value, match.Index);
                if (match.Value != "")
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
