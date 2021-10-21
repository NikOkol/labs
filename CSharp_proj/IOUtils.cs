using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_proj.Validation;

namespace CSharp_proj
{
    static class IOUtils
    {
        public static int Int_input(string message, ISpecification<int> specification = null)
        {
            Console.WriteLine(message);
            while (true)
            {
                int digit;
                if (int.TryParse(Console.ReadLine(), out digit))
                {
                    try
                    {
                        if (specification != null)
                        {
                            specification.Validate(digit);
                        }
                        return digit;
                    }
                    catch (ValidationException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect value");
                }
            }
        }

        public static double Double_input(string message, ISpecification<double> specification = null)
        {
            Console.WriteLine(message);
            while (true)
            {
                double digit;
                if (double.TryParse(Console.ReadLine(), out digit))
                {
                    try
                    {
                        if (specification != null)
                        {
                            specification.Validate(digit);
                        }
                        return digit;
                    }
                    catch(ValidationException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect value");
                }
            }
        }
        
        public static DateTime Date_input(string message)
        {
            Console.WriteLine(message);
            while (true)
            {
                string sValue = Console.ReadLine();
                try
                {
                    DateTime date = DateTime.ParseExact(sValue, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                    return date;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        public static string String_input(string message)
        {
            Console.WriteLine(message);
            ISpecification<string> specification = new IsNotEmptyString();
            while (true)
            {
                string Str = Console.ReadLine();
                try
                {
                    specification.Validate(Str);

                    return Str;
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }


    }
}
