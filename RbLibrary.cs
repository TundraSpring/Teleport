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

            roomBridge.SetData("A01-R01-RB01", 1, "res://area_2.tscn", 1);

            List<RoomBridge> roomBridges = new List<RoomBridge> { roomBridge };

            library.Add(1, roomBridges);
        }
    }
}
