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

        //Action<Entity> defaultSelect, defaultDeselect;

        public MenuGroup(MenuOrder menuOrder = MenuOrder.vertical)
        /*
        public MenuGroup(
            MenuOrder menuOrder = MenuOrder.vertical,
            Action<Entity> defaultSelect = null,
            Action<Entity> defaultDeselect = null
            )
        */
        {
            menuList = new List<Entity>();
            this.menuOrder = menuOrder;
            //if (defaultSelect != null) this.defaultSelect = defaultSelect;
            //if (defaultDeselect != null) this.defaultDeselect = defaultDeselect;
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
            //entity.MenuControl.SetSelect(Select);
            //entity.MenuControl.SetDeselect(Deselect);
            //if (defaultSelect != null) entity.MenuControl.SetSelect(defaultSelect);
            //if (defaultDeselect != null) entity.MenuControl.SetDeselect(defaultDeselect);

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

        public Entity Last()
        {
            return menuList.Last();
        }

        /*
        // Placeholder functions for handling visual state changes of menu items.
        public void Select(Entity entity)
        {
            entity.Visible = true;
        }
        public void Deselect(Entity entity)
        {
            entity.Visible = false;
        }
        */

        public void SetSelect(Action<Entity> method)
        {
            foreach (Entity entity in menuList)
            {
                entity.MenuControl.SetSelect(method);
            }
        }
        public void SetSoftSelect(Action<Entity> method)
        {
            foreach (Entity entity in menuList)
            {
                entity.MenuControl.SetSoftSelect(method);
            }
        }
        public void SetDeselect(Action<Entity> method)
        {
            foreach (Entity entity in menuList)
            {
                entity.MenuControl.SetDeselect(method);
            }
        }
        public void SetSoftDeselect(Action<Entity> method)
        {
            foreach (Entity entity in menuList)
            {
                entity.MenuControl.SetSoftDeselect(method);
            }
        }

        public void Refresh()
        {
            foreach (Entity entity in menuList)
            {
                if (entity == current)
                    entity.MenuControl.SoftSelect();
                else
                    entity.MenuControl.SoftDeselect();
            }
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

        public bool CheckOrSelectLast()
        {
            if (current == menuList.Last())
                return true;
            current.MenuControl.Deselect();
            current = menuList.Last();
            current.MenuControl.Select();
            return false;
        }
    }
}
