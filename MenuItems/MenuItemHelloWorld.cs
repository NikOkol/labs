using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.MenuItems
{
    public class MenuItemHelloWorld : MenuItemCore
    {
        public override string Title { get { return "Hello world!"; } }

        public override void Execute()
        {
            Console.WriteLine("Hello world!");
        }
    }
}
