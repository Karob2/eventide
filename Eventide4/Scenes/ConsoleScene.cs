using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eventide4.Scenes
{
    public class ConsoleScene : Scene
    {
        Systems.Entity inputText;
        Systems.Entity logText;
        StringBuilder inputMessage;

        Util.Console console;

        SpriteFont font;
        int areaX, areaY, areaWidth, areaHeight, maxLines;

        public ConsoleScene() : base(false)
        {
            sceneType = SceneType.Menu;

            logText = new Systems.Entity(this, entityList).AddText("system:default", "", 5f, 25f, Color.White, 1f);
            inputText = new Systems.Entity(this, entityList).AddText("system:default", "", 5f, 205f, Color.Cyan, 1f);
            inputMessage = new StringBuilder("");

            // TODO: This is such a messy pile of public properties:
            font = logText.TextState.Font.SpriteFont;

            SetupVisuals();

            console = new Util.Console();
            UpdateLog();

            GlobalServices.TextHandler.Start();
        }

        void SetupVisuals()
        {
            int gameWidth = GlobalServices.Game.GraphicsDevice.Viewport.Width;
            int gameHeight = GlobalServices.Game.GraphicsDevice.Viewport.Height;
            areaWidth = gameWidth * 4 / 6;
            areaHeight = gameHeight * 4 / 6;
            areaX = gameWidth / 6;
            areaY = gameHeight / 6;

            int textHeight = font.LineSpacing;
            maxLines = areaHeight / textHeight;

            logText.TextState.X = (float)(areaX + 5);
            logText.TextState.Y = (float)(areaY + 5);
            inputText.TextState.X = (float)(areaX + 5);
            inputText.TextState.Y = (float)(areaY + areaHeight - textHeight - 5);
        }

        public override void UpdateControl()
        {
            GlobalServices.TextHandler.DumpBuffer();
            base.UpdateControl();

            foreach (char c in GlobalServices.TextHandler.TextBuffer)
            {
                if (c == '\n' || c == '\r')
                {
                    // TODO: Do I really need to allow forced line breaks?
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                        inputMessage.Append('\n');
                }
                else if (Char.IsLetterOrDigit(c) || Char.IsPunctuation(c) || Char.IsSymbol(c))
                {
                    // TODO: Strip off characters out of the font range? (Convert to question mark or similar.)
                    inputMessage.Append(c);
                }
                else if (Char.IsWhiteSpace(c))
                {
                    inputMessage.Append(' ');
                }
                else if (c == '\b')
                {
                    if (inputMessage.Length > 0)
                        inputMessage.Remove(inputMessage.Length - 1, 1);
                }
                /*
                else
                {
                    // To figure out which keys are being registered and what their codes are.
                    inputMessage.Append("(" + String.Format("0x{0:X}", Convert.ToByte(c)) + ")");
                }
                */
                //inputMessage.Append("(" + String.Format("0x{0:X}", Convert.ToByte(c)) + ")");
            }
            inputText.SetText(inputMessage.ToString());

            if (!(Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.RightControl)))
            {
                if (GlobalServices.InputManager.JustPressed(Input.GameCommand.ConsoleConfirm))
                {
                    console.LogMessage(inputMessage.ToString());
                    inputMessage.Clear();
                }
            }
            if (GlobalServices.InputManager.JustPressed(Input.GameCommand.ConsoleCancel))
            {
                if (inputMessage.Length == 0)
                {
                    Scene.RemoveScene(this);
                }
                else
                {
                    inputMessage.Clear();
                }
            }
        }

        public override void Render()
        {
            if (console.HasUpdate()) UpdateLog();

            Texture2D pixel = new Texture2D(GlobalServices.Game.GraphicsDevice, 1, 1);
            Color[] colorData = {
                        Color.White
                    };
            pixel.SetData<Color>(colorData);
            GlobalServices.SpriteBatch.Draw(pixel, new Rectangle(areaX, areaY, areaWidth, areaHeight), new Color(0.0f, 0.0f, 0.5f, 0.5f));
            base.Render();
        }

        void UpdateLog()
        {
            StringBuilder sb = new StringBuilder();
            string test = console.ReadLast();
            //int maxLines = 
            while (test != null)
            {
                sb.Insert(0, test);
                test = console.ReadPrevious();
                if (test != null) sb.Insert(0, '\n');
            }
            logText.SetText(sb.ToString());

            //WrappedText wt = new WrappedText(font, )
        }
    }

    /*
    public class WrappedText
    {
        SpriteFont font;
        string text;
        float width;
        float height;
        //int fontHeight;
        List<string> lines;

        public WrappedText(SpriteFont font, string text, float width, float height = 0f)
        {
            this.font = font;
            this.text = text;
            this.width = width;
            this.height = height;
            //fontHeight = font.LineSpacing;

            Calculate();
        }

        public string LastNLines(int n, out float height)
        {
            if (lines.Count() == 0)
            {
                height = 0f;
                return "";
            }
            StringBuilder sb = new StringBuilder();
            int i1 = Math.Max(lines.Count() - n, 0);
            for (int i = i1; i < lines.Count(); i++)
            {
                if (i > i1)
                    sb.Append('\n');
                sb.Append(lines[i]);
            }

            height = (float)((lines.Count() - i1) * font.LineSpacing);
            return sb.ToString();
        }

        public string LastLines(out float height)
        {
            if (lines.Count() == 0)
            {
                height = 0f;
                return "";
            }
            StringBuilder sb = new StringBuilder();
            int n = (int)this.height / font.LineSpacing;
            int i1 = Math.Max(lines.Count() - n, 0);
            for (int i = i1; i < lines.Count(); i++)
            {
                if (i > i1)
                    sb.Append('\n');
                sb.Append(lines[i]);
            }

            height = (float)((lines.Count() - i1) * font.LineSpacing);
            return sb.ToString();
        }

        public void Calculate()
        {
            // TODO: Safeguard against alternate whitespace.
            lines = new List<string>();
            string test = "";
            float testLength = font.MeasureString(test).X;
            string cap = "";
            float capLength;
            for (int i = 0; i < text.Length; i++)
            {
                capLength = font.MeasureString(cap).X;
                if (testLength + capLength >= width)
                {
                    if (capLength >= width * 0.25f)
                    {
                        lines.Add(test + cap.Substring(0, cap.Length - 1));
                        test = "";
                        testLength = 0f;
                        cap = cap.Substring(cap.Length - 1, 1);
                        if (cap.Equals(" ")) cap = "";
                    }
                    else
                    {
                        lines.Add(test);
                        test = "";
                        testLength = 0f;
                    }
                }
                if (text[i] == ' ')
                {
                    test = test + cap + ' ';
                    testLength = font.MeasureString(test).X;
                    cap = "";
                }
                else if (text[i] == '\n' || text[i] == '\r')
                {
                    lines.Add(test + cap);
                    test = "";
                    testLength = 0f;
                    cap = "";
                }
                else
                {
                    cap = cap + text[i];
                }
            }
        }
    }
    */
}
