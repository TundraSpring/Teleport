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

        static string nextDestinationId;
        static int nextDestinationEntrance;


        public void SetNextDestination(string newDestinationId, int newDestinationEntrance)
        {
            nextDestinationId = newDestinationId;
            nextDestinationEntrance = newDestinationEntrance;
        }

        public override void _Ready()
        {

        }

        public string GetNextRoomDestination()
        {
            return nextDestinationId;
        }
    }
}
