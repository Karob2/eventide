using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide4.Library
{
    public class TextureLibrary : Library
    {
        #region static
        /*

            STATIC PARAMETERS AND METHODS

        */

        static List<TextureLibrary> libraries;

        // This class must be Initialized before any other methods are called.
        // Currently handled automatically by Library.Initialize();
        public static new void Initialize()
        {
            libraries = new List<TextureLibrary>();
        }

        // Libraries should always be created using this function to ensure they are added to the static list.
        public static TextureLibrary NewLibrary()
        {
            TextureLibrary library = new TextureLibrary();
            libraries.Add(library);
            return library;
        }

        // Libraries should always be removed from the static list via this function when they are no longer needed.
        // Otherwise, unneeded textures will not be garbage collected.
        public static void RemoveLibrary(TextureLibrary library)
        {
            libraries.Remove(library);
        }
        #endregion

        #region local
        /*

            LOCAL PARAMETERS AND METHODS

        */

        Dictionary<string, Texture2D> textureList;

        // This method checks if a texture has been loaded already, loads if necessary, then returns the reference.
        // --The local library is checked first.
        // --If a texture reference is found in another library, the reference is copied to the local library.
        public Texture2D RegisterTexture(string path)
        {
            Texture2D texture;

            // Search for loaded texture in local library and return if found.
            if (textureList.TryGetValue(path, out texture)) return texture;

            // Search for loaded texture in all texture libraries and copy reference to local library if found.
            foreach (TextureLibrary tLibrary in libraries)
            {
                if (tLibrary == this) continue;
                if (tLibrary.textureList.TryGetValue(path, out texture))
                {
                    textureList.Add(path, texture);
                    return texture;
                }
            }

            // Otherwise, load the texture and store a reference in the local library.
#if DEBUG
            string fullPath = Program.contentDirectory + path;
            if (File.Exists(path + ".jpg"))
                fullPath = path + ".jpg";
            else
                fullPath = path + ".png";
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            texture = Texture2D.FromStream(GlobalServices.Game.GraphicsDevice, fileStream);
            fileStream.Dispose();
#else
            texture = GlobalServices.Content.Load<Texture2D>(path);
#endif
            textureList.Add(path, texture);
            return texture;
        }

        // Removes the specified texture from the local library.
        // --Should only be used if absolutely certain the texture isn't being used locally and won't be used again soon.
        // --Should only be used when large amounts of data should be released prematurely for performance reasons.
        // Generally, garbage collection is sufficient to handle necessary removal whenever texture libraries are deleted.
        public void RemoveTexture(string path)
        {
            textureList.Remove(path);
        }
        // Slower alternative method.
        public void RemoveTexture(Texture2D texture)
        {
            foreach (string path in textureList.Keys)
            {
                if (textureList[path] == texture)
                {
                    // Only delete the first matching instance, because multiple local instances should not exist.
                    textureList.Remove(path);
                    break;
                }
            }
        }

        // Reset the local library, leaving dereferenced textures to be garbage collected.
        public void Reset()
        {
            textureList.Clear();
        }
        #endregion
    }
}
