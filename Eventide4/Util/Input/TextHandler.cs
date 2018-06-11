using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Util.Input
{
    public class TextHandler
    {
        /*
        EventHandler<TextInputEventArgs> onTextEntered;
        KeyboardState _prevKeyState;
        */
        char[] textBuffer;
        int bufferSize;
        const int maxBufferSize = 32;
        List<char> textBufferClone;
        bool running;

        public List<char> TextBuffer
        {
            get
            {
                return textBufferClone;
            }
        }

        public TextHandler()
        {
            textBuffer = new char[maxBufferSize];
            textBufferClone = new List<char>();
            running = false;
        }

        public void Start()
        {
            if (running) return;
            /*
            #if OpenGL
                        Window.TextInput += TextEntered;
                        onTextEntered += HandleInput;
            #else
                        GlobalServices.Game.Window.TextInput += HandleInput;
            #endif
            */
            GlobalServices.Game.Window.TextInput += HandleInput;
            textBufferClone.Clear();
            bufferSize = 0;
            running = true;
        }

        public void Reset()
        {
            bufferSize = 0;
        }

        public void Stop()
        {
            if (!running) return;
            GlobalServices.Game.Window.TextInput -= HandleInput;
            textBufferClone.Clear();
            running = false;
        }

        /*
        private void TextEntered(object sender, TextInputEventArgs e)
        {
            if (onTextEntered != null)
                onTextEntered.Invoke(sender, e);
        }
        */

        private void HandleInput(object sender, TextInputEventArgs e)
        {
            if (bufferSize < maxBufferSize)
            {
                textBuffer[bufferSize] = e.Character;
                bufferSize++;
            }
        }

        /*
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

#if OpenGL
            if (keyState.IsKeyDown(Keys.Back) && _prevKeyState.IsKeyUp(Keys.Back))
            {
                onTextEntered.Invoke(this, new TextInputEventArgs('\b'));
            }
            if (keyState.IsKeyDown(Keys.Enter) && _prevKeyState.IsKeyUp(Keys.Enter))
            {
                onTextEntered.Invoke(this, new TextInputEventArgs('\r'));
            }

            // Handle other special characters here (such as tab )
#endif

            _prevKeyState = keyState;
        }
        */

        public void DumpBuffer()
        {
            textBufferClone.Clear();
            for (int i = 0; i < bufferSize; i++)
            {
                textBufferClone.Add(textBuffer[i]);
            }
            bufferSize = 0;
        }
    }
}
