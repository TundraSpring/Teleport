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

        RoomBridge nextRoom;
        Player player = new Player();
        //Dictionary<int, List<Dygnflower>> flowerLibrary = new Dictionary<int, List<Dygnflower>>();
        //Dictionary<int, List<RoomBridge>> rbLibrary = new Dictionary<int, List<RoomBridge>>();
        Dictionary<string, RoomPacket> roomLibrary = new Dictionary<string, RoomPacket>();

        public void SetNextDestination(RoomBridge newRoomLoadInstructions)
        {
            nextRoom = newRoomLoadInstructions;
        }

        public override void _Ready()
        {
            //nextRoomLoadInstructions = new RoomBridge();
            //nextRoomLoadInstructions.SetData("", -1);

            player = new();

            Dygnflower dygnflower1 = new();
            Dygnflower dygnflower2 = new();
            dygnflower1.SetData("A01-R01-F01", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-9452, -16), false);
            dygnflower2.SetData("A01-R01-F02", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-1180, -99), false);
            List<Dygnflower> dygnflowers = new() { dygnflower1, dygnflower2 };

            RoomBridge roomBridge = new("res://area_2.tscn", 0);
            List<RoomBridge> roomBridges = new() { roomBridge };

            Vector4 cameraLimits = new(-10000, -1600, 1260, 250);

            List<Vector2> playerSpawnPoints = new() { new Vector2(0, 0) };
            RoomPacket roomPacket = new(dygnflowers, roomBridges, cameraLimits, playerSpawnPoints);

            

            roomLibrary.Add("A01-R01", roomPacket);

            nextRoom = new("", 0);
        }

        public string GetNextDestinationScene()
        {
            return nextRoom.nextDestinationScene;
        }

        public int GetNewRoomSpawnPos()
        {
            return nextRoom.spawnPos;
        }

        public void SetPlayer(Player newPlayer)
        {
            player = newPlayer;
        }

        public Player GetPlayer()
        {
            return player;
        }





        public void UpdateRoomLibrary(string roomId, List<Dygnflower> dygnFlowers)
        {
            roomLibrary[roomId].UpdateDygnflowers(dygnFlowers);
        }

        public void UpdatePendingFlowers(string roomId)
        {
            int nonSleepingCount = 0;
            List<Dygnflower> flowers = roomLibrary[roomId].dygnflowers;

            for (int i = 0; i < flowers.Count; i++)
            {
                if (flowers[i].status != DygnflowerStatus.Sleeping)
                {
                    nonSleepingCount++;
                }
            }
            if (nonSleepingCount == 2)
            {
                for (int i = 0; i < flowers.Count; i++)
                {
                    if (flowers[i].status != DygnflowerStatus.Sleeping)
                    {
                        flowers[i].SetStatus(DygnflowerStatus.Active);
                    }
                }
            }
            Global.Instance.UpdateRoomLibrary(roomId, flowers);
        }

        public void UpdateRoomLibrary(string roomId, List<RoomBridge> RBs)
        {
            roomLibrary[roomId].UpdateRBs(RBs);
        }

        public RoomPacket GetRoomPacket(string roomId)
        {
            return roomLibrary[roomId];
        }

        //public void UpdateLibrary(int roomId, List<IGameObject> objects)
        //{
        //    if (objects[0].GetType() == typeof(Dygnflower))
        //    {
        //        List<Dygnflower> list = new List<Dygnflower>();
        //        for (int i = 0; i < objects.Count; i++)
        //        {
        //            list.Add(objects[i] as Dygnflower);
        //        }
        //        flowerLibrary[roomId] = list;
        //    }
        //    if (objects[0].GetType() == typeof(RoomBridge))
        //    {
        //        List<RoomBridge> list = new List<RoomBridge>();
        //        for (int i = 0; i < objects.Count; i++)
        //        {
        //            list.Add(objects[i] as RoomBridge);
        //        }
        //        rbLibrary[roomId] = list;
        //    }
        //}

    }
}
