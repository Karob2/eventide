using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class MenuComponent : Component, IUpdateComponent
    {
        /*
        public MenuComponent()
        {
            if(MenuGroup.activeMenuGroup != null)
            {
                MenuGroup.activeMenuGroup.menuList.Add(this);
            }
        }
        */
        public MenuComponent(MenuGroup menuGroup)
        {
            menuGroup.menuList.Add(this);
        }

        public void Update()
        {
            Rectangle boundary = host.renderComponent.Request(RenderProperty.Boundary);
            if (ContentHandler.mouseX >= boundary.X && ContentHandler.mouseX < boundary.X + boundary.Width
                && ContentHandler.mouseY >= boundary.Y && ContentHandler.mouseY < boundary.Y + boundary.Height)
            {
                //((TextComponent)(host.renderComponent).)
            }
        }
    }

    public class MenuGroup
    {
        public static MenuGroup activeMenuGroup;

        public List<MenuComponent> menuList;
        public int currentItem;

        public MenuGroup()
        {
            menuList = new List<MenuComponent>();
            //currentItem = _currentItem;
        }
        /*
        public void Start()
        {
            activeMenuGroup = this;
        }

        public void End()
        {
            activeMenuGroup = null;
        }
        */
    }
}
