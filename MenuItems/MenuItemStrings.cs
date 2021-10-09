using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.MenuItems
{
    class MenuItemStrings : MenuItemCore
    {
        public override string Title { get { return "Strings"; } }

        public override void Execute()
        {
            Console.WriteLine("Enter first string: ");
            string str1 = Console.ReadLine();
            Console.WriteLine("Enter second string: ");
            string str2 = Console.ReadLine();
            if (String.Compare(str1, str2) == 0)
            {
                Console.WriteLine("(+)Strings are equal.");
            }
            else
            {
                Console.WriteLine("(-)Strings are not equal.");
            }
            IOUtils.EditedStringsCompare(str1, str2);
            IOUtils.StringReverseCompare(str1, str2);
            if (IOUtils.MatchesSearch(str1))
            {
                Console.WriteLine("...in first string");
            }
            else
            {
                Console.WriteLine("(-)No emails/phone numbers/IP adresses in first string.");
            }
            if (IOUtils.MatchesSearch(str2))
            {
                Console.WriteLine("...in second string");
            }
            else
            {
                Console.WriteLine("(-)No emails/phone numbers/IP adresses in second string.");
            }
        }
    }
}