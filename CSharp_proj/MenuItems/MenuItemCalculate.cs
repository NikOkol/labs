using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_proj.Validation;

namespace CSharp_proj.MenuItems
{
    class MenuItemCalculate : MenuItemCore
    {
        public override string Title { get { return "Calc: (x + Sqrt(y)) % z"; } }

        public override void Execute()
        {
            double x, y, z;
            x = IOUtils.Double_input("Enter X = ");
            y = IOUtils.Double_input("Enter Y = ", new IsNotLessThanZero());
            z = IOUtils.Double_input("Enter Z = ", new IsNotZero());
            double result = (x + Math.Sqrt(y)) % z;
            string print_result = string.Format("{0:0.000}", result);
            Console.WriteLine("Your result = " + print_result);
        }
    }
}
