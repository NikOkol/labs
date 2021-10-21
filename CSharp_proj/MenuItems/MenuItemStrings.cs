using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CSharp_proj.Validation;

namespace CSharp_proj.MenuItems
{
    class MenuItemStrings : MenuItemCore
    {
        public override string Title { get { return "Strings"; } }

        public override void Execute()
        {
            string str1 = IOUtils.String_input("Enter first string: ");
            string str2 = IOUtils.String_input("Enter second string: ");

            StringsCompare(str1, str2);

            EditedStringsCompare(str1, str2);

            StringReverseCompare(str1, str2);

            MatchesSearch(str1);
            MatchesSearch(str2);
        }

        private static void StringsCompare(string str1, string str2)
        {
            ISpecification<int> specification = new StringsNotEqual();
            try
            {
                specification.Validate(String.Compare(str1, str2));
                Console.WriteLine("(+)Strings are equal.");
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void EditedStringsCompare(string str1, string str2)
        {

            str1 = str1.Trim();
            str2 = str2.Trim();
            str1 = Regex.Replace(str1, @"\s+", " ");
            str2 = Regex.Replace(str2, @"\s+", " ");
            str1 = str1.ToLower();
            str2 = str2.ToLower();
            ISpecification<int> specification = new EditedStringsNotEqual();
            try
            {
                specification.Validate(String.Compare(str1, str2));
                Console.WriteLine("(+)Edited strings are equal.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void StringReverseCompare(string str1, string str2)
        {
            char[] astr = str1.ToCharArray();
            Array.Reverse(astr);
            str1 = new string(astr);
            ISpecification<int> specification = new ReversedStringsNotEqual();
            try
            {
                specification.Validate(String.Compare(str1, str2));
                Console.WriteLine("(+)Reversed strings are equal.");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void MatchesSearch(string str)
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
            ISpecification<bool> specification = new MatchesNotFound();
            try
            {
                specification.Validate(result);
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message + "'" + str + "' string.");
            }
        }
    }
}