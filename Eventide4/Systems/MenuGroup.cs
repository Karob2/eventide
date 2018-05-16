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

    public class MenuGroup
    {
        List<Entity> menuList;
        Entity current;
        MenuOrder menuOrder;

        public MenuGroup(MenuOrder menuOrder = MenuOrder.vertical)
        {
            menuList = new List<Entity>();
            this.menuOrder = menuOrder;
        }

        public void Add(Entity entity)
        {
            menuList.Add(entity);
            if (entity.MenuControl == null)
            {
                entity.AddMenuControl(new MenuControl(entity));
            }
            if (menuOrder == MenuOrder.vertical)
            {
                entity.MenuControl.SetAction(KeyType.MenuUp, Previous);
                entity.MenuControl.SetAction(KeyType.MenuDown, Next);
            }
            else
            {
                entity.MenuControl.SetAction(KeyType.MenuLeft, Previous);
                entity.MenuControl.SetAction(KeyType.MenuRight, Next);
            }
            entity.MenuControl.SetSelect(Select);
            entity.MenuControl.SetDeselect(Deselect);

            // Select first menu item by default.
            if (menuList.Count == 1)
            {
                current = entity;
                entity.MenuControl.SoftSelect();
            }
            else
            {
                entity.MenuControl.SoftDeselect();
            }
        }

        // Placeholder functions for handling visual state changes of menu items.
        public void Select(Entity entity)
        {
            entity.Visible = true;
        }
        public void Deselect(Entity entity)
        {
            entity.Visible = false;
        }

        public void Next(Entity entity = null)
        {
            current.MenuControl.Deselect();
            int index = menuList.IndexOf(current);
            if (index == menuList.Count - 1)
                index = 0;
            else
                index = index + 1;
            current = menuList[index];
            current.MenuControl.Select();
        }

        public void Previous(Entity entity = null)
        {
            current.MenuControl.Deselect();
            int index = menuList.IndexOf(current);
            if (index == 0)
                index = menuList.Count - 1;
            else
                index = index - 1;
            current = menuList[index];
            current.MenuControl.Select();
        }
    }
}
