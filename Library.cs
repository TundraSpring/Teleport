using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleport
{
    public partial class Library<T> : Node
    {
        public static Dictionary<int, List<T>> library = new Dictionary<int, List<T>>();

        public void UpdateLibrary(int roomId, List<T> objects)
        {
            library[roomId] = objects;
        }

        public List<T> GetRoomData(int roomId)
        {
            return library[roomId];
        }
    }
}
