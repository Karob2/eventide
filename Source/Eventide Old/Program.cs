// Eventide
// Contributors: Kristopher McKenzie
// License: MIT
// Project start: 2018-04-17

// Currently, DEBUG builds load some assets from a fixed directory instead of compiled XNB files.
// This is to avoid having to frequently go through the monogame pipeline manager to modify the asset bank.
// Eventually I may implement a non-debug option to load raw assets.
//#undef DEBUG

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Eventide
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
