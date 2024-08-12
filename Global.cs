using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleport
{
    public partial class Global : Node
    {
        private static Global _instance;
        public static Global Instance => _instance;
        public override void _EnterTree()
        {
            if (_instance != null)
            {
                this.QueueFree();
            }
            _instance = this;
        }

        RoomBridge nextRoomLoadInstructions;
        Player player = new Player();

        public void SetNextDestination(RoomBridge newRoomLoadInstructions)
        {
            nextRoomLoadInstructions = newRoomLoadInstructions;
        }

        public override void _Ready()
        {
            nextRoomLoadInstructions = new RoomBridge();
            nextRoomLoadInstructions.SetData("", "A01-R01", 1, 2, 1, new Vector4(-10000, -1600, 1260, 250), new Vector2(0, 0));

            player = new Player();
        }

        public string GetNextDestinationScene()
        {
            return nextRoomLoadInstructions.nextDestinationScene;
        }

        public RoomBridge GetNextRoomLoadInstructions()
        {
            return nextRoomLoadInstructions;
        }

        public void SetPlayer(Player newPlayer)
        {
            player = newPlayer;
        }

        public Player GetPlayer()
        {
            return player;
        }
    }
}
