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

namespace Eventide4
{
    public class Pathfinder
    {
        public enum FileType
        {
            xml,
            image,
            xnb,
            custom
        }

        string _namespace;
        string collection;
        List<string> sub;
        string file;
        FileType type;
        string ext;
        string path;
        string contentPath;
        //public string _Namespace { get { return _namespace; } set { _namespace = value; } }
        //public string Collection { get { return collection; } set { collection = value; } }
        public string Ext { get { return ext; } set { ext = value; } }
        public string Path { get { return path; } set { path = value; } }
        public string ContentFile { get { return file; } set { file = value; } }
        public string ContentPath { get { return contentPath; } set { contentPath = value; } }

        static Pathfinder currentPath;

        public Pathfinder()
        {
        }

        /*
        public Pathfinder(string _namespace, string collection, List<string> sub, string file, Pathfinder.FileType type)
        {
            this._namespace = _namespace;
            this.collection = collection;
            this.sub = new List<string>(sub); // make a duplicate instead of copying the reference
            this.file = file;
            this.type = type;
            //this.ext = ext;
        }
        */

        public string GetContentPath()
        {
            return System.IO.Path.GetFullPath(System.IO.Path.Combine());
        }

        public static void SetCurrentPath(Pathfinder pathfinder)
        {
            currentPath = pathfinder;
        }

        public static void ClearCurrentPath()
        {
            currentPath = null;
        }

        void FindPath()
        {
            string[] ext;
            switch (type)
            {
                case FileType.xml:
                    ext = new string[] { "xml" };
                    break;
                case FileType.image:
                    ext = new string[] { "xnb", "png", "jpg" };
                    break;
                case FileType.custom:
                    ext = new string[] { this.ext };
                    break;
                default:
                    ext = new string[] { "xnb" };
                    break;
            }
            string path1;
            string path2;
            path1 = System.IO.Path.Combine(_namespace, collection, System.IO.Path.Combine(sub.ToArray()));
            foreach (string folder in GlobalServices.ExtensionDirectories)
            {
                for (int i = 0; i < ext.Length; i++)
                {
                    path2 = System.IO.Path.Combine(folder, path1, file + "." + ext[i]);
/*#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Searching " + path2);
#endif*/
                    if (File.Exists(path2))
                    {
                        this.ext = ext[i];
                        path = path2;
                        contentPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(folder, path1));
                        return;
                    }
                }
            }
            for (int i = 0; i < ext.Length; i++)
            {
                path2 = System.IO.Path.Combine(GlobalServices.ContentDirectory, path1, file + "." + ext[i]);
/*#if DEBUG
                System.Diagnostics.Debug.WriteLine("Searching " + path2);
#endif*/
                if (File.Exists(path2))
                {
                    this.ext = ext[i];
                    path = path2;
                    contentPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(GlobalServices.ContentDirectory, path1));
                    return;
                }
            }
            path = null;
            contentPath = null;
        }

        // Example: Pathfinder.Find("Eventide:common/ball", "sprites", Pathfinder.image).Path
        //public static Pathfinder Find(string locator, string collection, FileType type, Pathfinder current = null, string ext = null)
        public static Pathfinder Find(string locator, string collection, FileType type, string ext = null)
        {
            Pathfinder pathfinder = new Pathfinder();
            pathfinder.collection = collection;
            pathfinder.type = type;
            pathfinder.ext = ext;
            //string currentNamespace;
            //List<string> currentPath;
            if (currentPath == null)
            {
                pathfinder._namespace = "core";
                pathfinder.sub = new List<string>();
            }
            else
            {
                pathfinder._namespace = currentPath._namespace;
                pathfinder.sub = new List<string>(currentPath.sub); // make a duplicate instead of copying the reference
            }

            /*
            if (!ValidateLocator(locator))
            {
                // error
            }
            */

            string[] parts = locator.Split(':');
            if (parts.Length == 2)
            {
                pathfinder._namespace = parts[0];
            }
            else if (parts.Length != 1)
            {
                throw new InvalidFileException("Duplicate namespace marker ':' in '" + locator + "'.");
            }
            string[] parts2 = parts[parts.Length - 1].Split('/');
            pathfinder.file = parts2[parts2.Length - 1];
            if (parts2.Length > 1)
            {
                if (parts2[0].Equals("")) pathfinder.sub = new List<string>();
                for (int i = 0; i < parts2.Length - 1; i++)
                {
                    if (parts2[i].Equals(".."))
                    {
                        if (pathfinder.sub.Count < 1)
                        {
                            throw new InvalidFileException("Path out of resource folder with excessive ':' in '" + locator + "'.");
                        }
                        pathfinder.sub.RemoveAt(pathfinder.sub.Count - 1);
                    }
                    else if (!parts2[i].Equals(""))
                    {
                        pathfinder.sub.Add(parts2[i]);
                    }
                }
            }
            /*
            for (int i = 0; i < ext.Length; i++)
            {
                if (File.Exists(Path.Combine(GlobalServices.ContentDirectory, )))
            }
            */
            if (!Validate(pathfinder._namespace))
            {
                throw new InvalidFileException("Invalid namespace character in '" + pathfinder._namespace + "' via '" + locator + "'.");
            }
            foreach (string str in pathfinder.sub)
            {
                if (!Validate(str))
                {
                    throw new InvalidFileException("Invalid path character in '" + str + "' via '" + locator + "'.");
                }
            }
            if (!Validate(pathfinder.file))
            {
                throw new InvalidFileException("Invalid filename character in '" + pathfinder.file + "' via '" + locator + "'.");
            }
            pathfinder.FindPath();
            return pathfinder;
        }

        /*
        static bool ValidateLocator(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '_') continue;
                if (str[i] == '/') continue;
                if (str[i] == ':') continue;
                if (str[i] < '0') return false;
                if (str[i] <= '9') continue;
                if (str[i] < 'A') return false;
                if (str[i] <= 'Z') continue;
                if (str[i] < 'a') return false;
                if (str[i] <= 'z') continue;
                return false;
            }
            return true;
        }
        */

        static bool Validate(string str)
        {
            if (str.Equals("")) return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '_') continue;
                if (str[i] < '0') return false;
                if (str[i] <= '9') continue;
                if (str[i] < 'A') return false;
                if (str[i] <= 'Z') continue;
                if (str[i] < 'a') return false;
                if (str[i] <= 'z') continue;
                return false;
            }
            return true;
        }
    }
}
