using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.MenuItems
{
    public class MenuItemExit : MenuItemCore
    {
        public override string Title { get { return "Exit"; } }

        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
