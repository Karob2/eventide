using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Systems
{
    
    public class KeyResponse
    {
        public Input.GameCommand Key { get; set; }
        public Action<Entity> Command { get; set; }

        public KeyResponse(Input.GameCommand key, Action<Entity> command)
        {
            Key = key;
            Command = command;
        }
    }

    public class MenuControl
    {
        Entity parent;
        List<KeyResponse> keyResponses;

        // "Soft Select" is when the item was selected by default, and thus should be highlighted without the usual fanfare.
        Action<Entity> OnSelect { get; set; }
        Action<Entity> OnSoftSelect { get; set; }
        Action<Entity> OnDeselect { get; set; }
        Action<Entity> OnSoftDeselect { get; set; }

        public MenuControl(Entity parent)
        {
            this.parent = parent;
            keyResponses = new List<KeyResponse>();
        }

        public MenuControl SetAction(Input.GameCommand key, Action<Entity> command)
        {
            foreach (KeyResponse kr in keyResponses)
            {
                if (kr.Key == key)
                {
                    kr.Command = command;
                    return this;
                }
            }
            keyResponses.Add(new KeyResponse(key, command));
            return this;
        }

        // Can be used to add multiple actions for a single key. TODO: Delete this?
        public MenuControl AddAction(Input.GameCommand key, Action<Entity> command)
        {
            keyResponses.Add(new KeyResponse(key, command));
            return this;
        }

        public MenuControl SetSelect(Action<Entity> command)
        {
            OnSelect = command;
            return this;
        }

        public MenuControl SetSoftSelect(Action<Entity> command)
        {
            OnSelect = command;
            return this;
        }

        public MenuControl SetDeselect(Action<Entity> command)
        {
            OnDeselect = command;
            return this;
        }

        public MenuControl SetSoftDeselect(Action<Entity> command)
        {
            OnSoftDeselect = command;
            return this;
        }

        // Add key input response commands to the method queue.
        public void Update()
        {
            foreach (KeyResponse kr in keyResponses)
            {
                if (GlobalServices.InputManager.Ticked(kr.Key))
                    if (kr.Command != null)
                        parent.Scene.QueueMethod(kr.Command, parent);
                    //kr.Command?.Invoke(parent);
            }
        }

        // It is okay for Select() and similar methods to invoke immediately since the invocation methods only alter
        //   visual states. In fact, because they modify the queue, they should not be queued unless a queue swap clone
        //   is made. And even in that case, the swap queue would need to be invoked immediately after the regular
        //   queue.

        public void Select()
        {
            parent.Active = true;
            /*
            if (OnSelect != null)
                parent.Scene.QueueMethod(OnSelect, parent);
            */
            OnSelect?.Invoke(parent);
        }

        public void SoftSelect()
        {
            parent.Active = true;
            /*
            if (OnSoftSelect != null)
                parent.Scene.QueueMethod(OnSoftSelect, parent);
            else
                Select();
            */
            if (OnSoftSelect == null)
                OnSelect?.Invoke(parent);
            else
                OnSoftSelect.Invoke(parent);
        }

        public void Deselect()
        {
            parent.Active = false;
            /*
            if (OnDeselect != null)
                parent.Scene.QueueMethod(OnDeselect, parent);
            */
            OnDeselect?.Invoke(parent);
        }

        public void SoftDeselect()
        {
            parent.Active = false;
            if (OnSoftDeselect == null)
                OnDeselect?.Invoke(parent);
            else
                OnSoftDeselect.Invoke(parent);
        }
    }
}
