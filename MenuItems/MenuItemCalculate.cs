using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.MenuItems
{
    class MenuItemCalculate : MenuItemCore
    {
        public override string Title { get { return "Calc: (x + Sqrt(y)) % z"; } }

        public override void Execute()
        {
            double x, y, z;
            Console.WriteLine("Enter X = ");
            x = IOUtils.Double_input();
            Console.WriteLine("Enter Y = ");
            y = IOUtils.ReadNotLessZeroValue();
            Console.WriteLine("Enter Z = ");
            z = IOUtils.ReadNotZeroValue();
            double result = (x + Math.Sqrt(y)) % z;
            string print_result = string.Format("{0:0.000}", result);
            Console.WriteLine("Your result = " + print_result);
        }
    }
}
