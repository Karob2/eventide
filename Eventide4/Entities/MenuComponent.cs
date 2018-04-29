using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4
{
    public class MenuComponent : IUpdateComponent
    {
        public MenuComponent()
        {
            if(MenuGroup.activeMenuGroup != null)
            {
                MenuGroup.activeMenuGroup.menuList.Add(this);
            }
        }

        public void Update()
        {

        }
    }

    public class MenuGroup
    {
        public static MenuGroup activeMenuGroup;

        public List<MenuComponent> menuList;
        public int currentItem;

        public MenuGroup(int _currentItem = 0)
        {
            menuList = new List<MenuComponent>();
            currentItem = _currentItem;
        }

        public void Start()
        {
            activeMenuGroup = this;
        }
    }
}
