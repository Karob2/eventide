using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    public enum MenuOrder
    {
        vertical,
        horizontal
    }

    public class MenuItem
    {
        public Entity EntityDefault { get; set; }
        public Entity EntitySelected { get; set; }

        public MenuItem(Entity entityDefault, Entity entitySelected)
        {
            EntityDefault = entityDefault;
            EntitySelected = entitySelected;
        }
    }

    public class MenuGroup
    {
        List<MenuItem> menuList;
        MenuItem current;
        MenuOrder menuOrder;

        public MenuGroup(MenuOrder menuOrder = MenuOrder.vertical)
        {
            menuList = new List<MenuItem>();
            this.menuOrder = menuOrder;
        }

        public void Add(Entity entityDefault, Entity entitySelected)
        {
            MenuItem menuItem = new MenuItem(entityDefault, entitySelected);
            if (menuList.Count == 0) current = menuItem;
            menuList.Add(menuItem);
            UpdateSelected();
        }

        public void Update()
        {
            // TODO: Need a proper key input handling system.
            if (menuOrder == MenuOrder.vertical && GlobalServices.KeyConfig.Down.Ticked()
                || menuOrder == MenuOrder.horizontal && GlobalServices.KeyConfig.Right.Ticked())
            {
                int index = menuList.IndexOf(current);
                if (index == menuList.Count - 1)
                    index = 0;
                else
                    index = index + 1;
                current = menuList[index];
            }
            else if (menuOrder == MenuOrder.vertical && GlobalServices.KeyConfig.Up.Ticked()
                || menuOrder == MenuOrder.horizontal && GlobalServices.KeyConfig.Left.Ticked())
            {
                int index = menuList.IndexOf(current);
                if (index == 0)
                    index = menuList.Count - 1;
                else
                    index = index - 1;
                current = menuList[index];
            }
            UpdateSelected();
        }

        void UpdateSelected()
        {
            foreach (MenuItem menuItem in menuList)
            {
                if (menuItem == current)
                {
                    menuItem.EntityDefault.Visible = false;
                    menuItem.EntitySelected.Visible = true;
                }
                else
                {
                    menuItem.EntityDefault.Visible = true;
                    menuItem.EntitySelected.Visible = false;
                }
            }
        }
    }
}
