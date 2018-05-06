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
    public class Library<K, T> where T : class
    {
        /*
        public static void Initialize()
        {
            GlobalServices.Content.RootDirectory = "Content";
            Library<K, T>.Initialize();
        }
        */

        #region static
        /*

            STATIC PARAMETERS AND METHODS

        */

        static List<Library<K, T>> libraries;

        // This class must be Initialized before any other methods are called.
        // Currently handled automatically by Library.Initialize();
        public static void Initialize()
        {
            libraries = new List<Library<K, T>>();
        }

        // Libraries should always be created using this function to ensure they are added to the static list.
        public static Library<K, T> NewLibrary()
        {
            Library<K, T> library = new Library<K, T>();
            libraries.Add(library);
            return library;
        }
        // This alternative allows the inheritors to have custom constructors.
        public static void AddLibrary(Library<K, T> library)
        {
            libraries.Add(library);
        }

        // Libraries should always be removed from the static list via this function when they are no longer needed.
        // Otherwise, unneeded textures will not be garbage collected.
        public static void RemoveLibrary(Library<K, T> library)
        {
            libraries.Remove(library);
        }
        #endregion

        #region local
        /*

            LOCAL PARAMETERS AND METHODS

        */

        Dictionary<K, T> list;

        public Library()
        {
            list = new Dictionary<K, T>();
        }

        // This method checks if a texture has been loaded already, loads if necessary, then returns the reference.
        // --The local library is checked first.
        // --If a texture reference is found in another library, the reference is copied to the local library.
        public T Register(K key)
        {
            T item;

            // Search for loaded texture in local library and return if found.
            if (list.TryGetValue(key, out item)) return item;

            // Search for loaded texture in all texture libraries and copy reference to local library if found.
            foreach (Library<K, T> tLibrary in libraries)
            {
                if (tLibrary == this) continue;
                if (tLibrary.list.TryGetValue(key, out item))
                {
                    list.Add(key, item);
                    return item;
                }
            }

            // Otherwise, load the texture and store a reference in the local library.
            Load(key);
            list.Add(key, item);
            return item;
        }

        protected virtual T Load(K key) { return default(T);  }

        // Removes the specified texture from the local library.
        // --Should only be used if absolutely certain the texture isn't being used locally and won't be used again soon.
        // --Should only be used when large amounts of data should be released prematurely for performance reasons.
        // Generally, garbage collection is sufficient to handle necessary removal whenever texture libraries are deleted.
        public void Remove(K key)
        {
            list.Remove(key);
        }
        // Slower alternative method.
        public void Remove(T item)
        {
            foreach (K key in list.Keys)
            {
                if (list[key].Equals(item))
                {
                    // Only delete the first matching instance, because multiple local instances should not exist.
                    list.Remove(key);
                    break;
                }
            }
        }

        // Reset the local library, leaving dereferenced textures to be garbage collected.
        public void Reset()
        {
            list.Clear();
        }
        #endregion
    }
}
