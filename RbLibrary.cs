using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleport
{
    public partial class RbLibrary : Library<RoomBridge>
    {
        private static RbLibrary _instance;
        public static RbLibrary Instance => _instance;
        public override void _EnterTree()
        {
            if (_instance != null)
            {
                this.QueueFree();
            }
            _instance = this;
        }

        public override void _Ready()
        {
            RoomBridge roomBridge = new RoomBridge();
            roomBridge.SetData("res://area_2.tscn", "A01-R01-RB01", 1, 0, 0, new Vector4(0, 0, 0, 0), new Vector2(0, 0));
            List<RoomBridge> roomBridges = new List<RoomBridge> { roomBridge };
            library.Add(1, roomBridges);
        }
    }
}
