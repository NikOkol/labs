using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_proj.MenuItems;

namespace CSharp_proj
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Menu.ClearItems();
                Menu.AddItem(new MenuItemExit());
                Menu.AddItem(new MenuItemHelloWorld());
                Menu.AddItem(new MenuItemCalculate());
                Menu.AddItem(new MenuItemRecursionDate());
                Menu.AddItem(new MenuItemStrings());
                Menu.Execute();
            }
        }



        
    }
}
