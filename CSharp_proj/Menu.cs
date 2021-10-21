using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_proj.MenuItems;

namespace CSharp_proj
{
    public class Menu
    {
        private static List<MenuItemCore> MenuItems = new List<MenuItemCore>();

        public static void ClearItems()
        {
            Menu.MenuItems.Clear();
        }

        public static void AddItem(MenuItemCore menuItem)
        {
            Menu.MenuItems.Add(menuItem);
        }
        public static void Execute()
        {
            Menu_print();
            int iMenu = IOUtils.Int_input("");
            if (iMenu >= 0 && iMenu < Menu.MenuItems.Count)
            {
                MenuItems.ToArray()[iMenu].Execute();
            }
            else
            {
                Console.WriteLine("Menu item not found.");
            }
        }

        private static void Menu_print()
        {
            Console.WriteLine("--------  Menu  --------");
            int iMenuItem = 0;
            foreach (MenuItemCore menuItem in Menu.MenuItems)
            {
                Console.WriteLine("{0}: {1}", iMenuItem++, menuItem.Title);
            }
        }
    }
}
