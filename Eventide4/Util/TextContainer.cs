using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Util
{
    public class TextLine
    {
        public string Text { get; set; }
        public List<string> WrappedText { get; set; }
        // TODO: Should I delete Rows in favor of WrappedText.Count? Probably not.
        public int Rows { get; set; }

        public TextLine(string text = "")
        {
            Text = text;
        }
    }

    public class TextContainer
    {
        Libraries.Font font;
        float width;
        float height;
        float lineSpacing;
        float avgCharWidth;
        //float maxCharWidth;
        LinkedList<TextLine> lines;

        bool wrappable;
        //bool scrollable;
        //bool alignTop;

        bool cursorVisible;
        bool cursorFollow;
        int cursorX;
        int cursorY;

        public TextContainer(Libraries.Font font, float width, float height = 0f, string text = null)
        {
            this.font = font;
            this.width = width;
            this.height = height;
            lineSpacing = (float)font.SpriteFont.LineSpacing;
            avgCharWidth = font.SpriteFont.MeasureString("Abcd efgh ijkl mnop, qrst uv wxyz.").X / 34f;
            //maxCharWidth = font.MeasureString("MMMMMMMMMMMMMMMMMMMM").X / 20f;

            wrappable = true;
            //scrollable = true;
            //alignTop = true;

            cursorX = 0;
            cursorY = 0;
            cursorVisible = true;
            cursorFollow = true;

            lines = new LinkedList<TextLine>();
            if (text != null)
            {
                lines.AddFirst(new TextLine(text));
                Update();
                cursorX = lines.Last.Value.Text.Length;
                cursorY = lines.Count - 1;
            }
        }

        public void SetSize(float width = -1f, float height = -1f)
        {
            if (height >= 0f) this.height = height;
            if (width >= 0f)
            {
                this.width = width;
                if (wrappable) UpdateWrapping();
            }
        }

        public void SetText(string text)
        {
            lines.Clear();
            lines.AddFirst(new TextLine(text));
            Update();
        }

        public void InsertText(string text)
        {
            if (lines.Count == 0)
            {
                lines.AddFirst(new TextLine(text));
                Update();
                cursorX = lines.First().Text.Length;
                cursorY = 0;
                return;
            }

            /*
            LinkedListNode<TextLine> line = lines.First;
            for (int i = 0; i < cursorY; i++)
            {
                if (line.)
            }
            */

            LinkedListNode<TextLine> line = GetCursorLine();
            line.Value.Text.Insert(cursorX, text);
            cursorX += line.Value.Text.Length;

            // Adjust cursorX overflow so it moves to the correct location in the next line when double-character newlines are encountered.
            // (Alternatively, I could sanitize input so double newlines are never encountered.)
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == '\r' && text[i + 1] == '\n')
                    cursorX--;
            }
            UpdateLine(line);
            UpdateCursor();
        }

        // Backspace key action.
        public void BackspaceText()
        {
            // TODO: Add "Selection" functionality (which would delete or type in place of the entire selection).
            if (lines.Count == 0) return;

            LinkedListNode<TextLine> line = GetCursorLine();
            if (cursorX == 0)
            {
                if (cursorY == 0) return;
                line.Previous.Value.Text += line.Value.Text;
                UpdateLine(line.Previous);
                cursorY--;
                cursorX = line.Previous.Value.Text.Length;
                lines.Remove(line);
                return;
            }
            line.Value.Text.Remove(cursorX - 1, 1);
            cursorX--;
            UpdateLine(line);
        }

        // Delete key action.
        public void DeleteText()
        {

        }

        LinkedListNode<TextLine> GetCursorLine()
        {
            // A bit hacky/redundant but nice and compact node lookup by index (without having to write a custom function).
            LinkedListNode<TextLine> line = lines.Find(lines.ElementAt(Math.Min(cursorY, lines.Count - 1)));
            if (cursorY >= lines.Count)
            {
                cursorX = line.Value.Text.Length;
                cursorY = lines.Count - 1;
            }
            else if (cursorX > line.Value.Text.Length)
            {
                cursorX = line.Value.Text.Length;
            }
            return line;
        }

        public void ScrollToCursor()
        {

        }

        public void ScrollToStart()
        {

        }

        public void ScrollToEnd()
        {

        }

        public void RenderText(float x, float y)
        {
            if (lines.Count == 0) return;
            // TODO: Add cropping.
            //SpriteBatch sb = new SpriteBatch(GlobalServices.Game.GraphicsDevice);
            //sb.Begin();
            float rowY = 0f;
            foreach (TextLine line in lines)
            {
                font.Render(line.Text, x, y + rowY, Color.White);
                rowY += lineSpacing;
                if (rowY >= height) return;
            }
        }

        void UpdateCursor()
        {
            if (lines.Count == 0) return;
            LinkedListNode<TextLine> line = lines.Find(lines.ElementAt(Math.Min(cursorY, lines.Count - 1)));
            if (cursorY >= lines.Count)
            {
                cursorX = line.Value.Text.Length;
                cursorY = lines.Count - 1;
            }
            while (cursorX > line.Value.Text.Length)
            {
                if (line.Next == null)
                {
                    cursorX = line.Value.Text.Length;
                    return;
                }
                cursorX = cursorX - line.Value.Text.Length - 1; //Line break counted as one character.
                line = line.Next;
            }
        }

        void Update()
        {
            LinkedListNode<TextLine> line = lines.First;
            //LinkedListNode<TextLine> nextLine;
            while (line != null)
            {
                //nextLine = line.Next;
                UpdateLine(line);
                //line = nextLine;
                line = line.Next;
            }
        }

        void UpdateWrapping()
        {
            LinkedListNode<TextLine> line = lines.First;
            while (line != null)
            {
                WrapLine(line.Value);
                line = line.Next;
            }
        }

        // If the line has line breaks, it is split into multiple lines, with original object reference pointing to the final line.
        // line = "Mocha\nBar" -> line.Prev = "Mocha", line = "Bar"
        void UpdateLine(LinkedListNode<TextLine> line)
        {
            int start = 0;
            bool first = true;
            LinkedListNode<TextLine> wrapLine = null;
            for (int i = 0; i < line.Value.Text.Length; i++)
            {
                if (line.Value.Text[i] == '\n' || line.Value.Text[i] == '\r')
                {
                    // TODO: Make sure this works for zero-length lines, e.g. the middle of "\n\n".
                    lines.AddBefore(line, new TextLine(line.Value.Text.Substring(start, i - start)));
                    if (first)
                    {
                        first = false;
                        wrapLine = line.Previous;
                    }
                    if (i + 1 < line.Value.Text.Length)
                    {
                        if (line.Value.Text[i] == '\r' && line.Value.Text[i + 1] == '\n')
                            i++;
                    }
                    start = i + 1;
                }
            }
            // TODO: Make sure this works for lines ending with "\n", since the final start is out-of-rage in that case.
            // Suggested fix if it does not work: if (start >= line.Text.Length) lines2.AddLast(new TextLine(""));
            //lines.AddBefore(nextLine, new TextLine(line.Value.Text.Substring(start, line.Value.Text.Length - start)));
            line.Value.Text = line.Value.Text.Substring(start, line.Value.Text.Length - start);
            if (first) wrapLine = line;

            if (wrappable)
            {
                while (wrapLine != line)
                {
                    WrapLine(wrapLine.Value);
                    wrapLine = wrapLine.Next;
                }
                WrapLine(wrapLine.Value);
            }
        }

        // Breaks a single string into individual strings at wrap points.
        // Method: Saunter back and forth using educational guesses until the exact point of wrapping is found.
        // TODO: This may perform poorly when ran frequently with large strings (such as typing text into a 10,000 byte string with no line breaks).
        // TODO: Add preferred wrapping at whitespace and symbols (and asian text?)
        void WrapLine(TextLine line)
        {
            int delta;
            bool forward;
            int testStart;
            int testN;

            line.WrappedText = new List<string>();
            if (line.Text.Length == 0)
            {
                line.WrappedText.Add("");
                line.Rows = 1;
                return;
            }

            testStart = 0;
            while (true)
            {
                // Estimate of character length that would reach the wrap point.
                testN = Math.Max(Math.Min((int)(width / avgCharWidth) - 2, line.Text.Length - testStart), 2);
                forward = true;
                delta = testN;

                while (true)
                {
                    if (TestWrap(line.Text.Substring(testStart, testN), out int offset))
                    {
                        // If stepping one charcter forwards and now long enough to wrap, commit.
                        if (forward && delta == 1)
                        {
                            testN--;
                            break;
                        }

                        delta = Math.Max(Math.Min(delta, offset), 1);

                        // If stepping forwards and now long enough to wrap, reverse direction and decrease increment.
                        if (forward)
                        {
                            forward = false;
                            delta = Math.Max(delta - 1, 1);
                        }

                        testN = Math.Max(testN - delta, 1);
                    }
                    else
                    {
                        // If at end of string and still not long enough to wrap, commit.
                        if (testN == line.Text.Length - testStart)
                        {
                            break;
                        }

                        // If stepping one character backwards and no longer long enough to wrap, commit.
                        if (!forward && delta == 1)
                        {
                            break;
                        }

                        delta = Math.Max(Math.Min(delta, offset), 1);

                        // If stepping backwards and no longer long enough to wrap, reverse direction and decrease increment.
                        if (!forward)
                        {
                            forward = true;
                            delta = Math.Max(delta - 1, 1);
                        }

                        testN = Math.Min(testN + delta, line.Text.Length - testStart);
                    }
                }

                line.WrappedText.Add(line.Text.Substring(testStart, testN));
                testStart = testStart + testN;
                if (testStart >= line.Text.Length) break;
            }

            line.Rows = line.WrappedText.Count;
        }

        bool TestWrap(string test, out int offset)
        {
            if (test.Length <= 1)
            {
                offset = 0;
                return false;
            }
            float testWidth = font.SpriteFont.MeasureString(test).X;
            offset = Math.Abs((int)((width - testWidth) / avgCharWidth));
            if (testWidth <= width) return false;
            return true;
        }
    }
}
