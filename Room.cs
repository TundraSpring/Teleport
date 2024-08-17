using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleport
{
    public partial class Room : Node2D
    {
        public string fullId;
        public List<Dygnflower> flowerObjects = new List<Dygnflower>();
        public List<RoomBridge> roomBridges = new List<RoomBridge>();
        public List<Vector2?> currentTeleportDestinations = new List<Vector2?>();
        public string nextRoomDestination = "";
        public int flowersInProximity = 0;
        public int roomId;
        public Player playerNodeObj;


        public override void _Ready()
        {
            RoomBridge dna = Global.Instance.GetNextRoomLoadInstructions();
            fullId = dna.fullId;
            roomId = dna.roomId;
            SetPlayer(dna);
            SetFlowers(dna.flowerAmo);
            SetRbs(dna.rbAmo);
            //playerNodeObj.SetSize(0.5F);
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            if (nextRoomDestination != "")
            {
                GetTree().ChangeSceneToFile(nextRoomDestination);
            }
        }


        public void SetFlowers(int flowerAmo)
        {
            List<Dygnflower> flowers = FlowerLibrary.Instance.GetRoomData(roomId);
            for (int i = 0; i < flowerAmo; i++)
            {
                Dygnflower flowerNodeObj = GetNode<Dygnflower>($"Dygnflower{i}");
                flowerNodeObj.SetData(flowers[i].id, flowers[i].channel, flowers[i].status, flowers[i].type, flowers[i].teleportDestination, true);
                flowerObjects.Add(flowerNodeObj);
            }
        }

        public void SetRbs(int rbAmo)
        {
            List<RoomBridge> bridges = RbLibrary.Instance.GetRoomData(roomId);
            for (int i = 0; i < rbAmo; i++)
            {
                RoomBridge rbObj = GetNode<RoomBridge>($"RoomBridge{i}");
                rbObj.SetData(bridges[i].nextDestinationScene, bridges[i].fullId, bridges[i].roomId, bridges[i].flowerAmo, bridges[i].rbAmo, bridges[i].cameraLimits, bridges[i].playerSpawn);
                roomBridges.Add(rbObj);
            }
        }

        public void SetPlayer(RoomBridge dna)
        {
            playerNodeObj = GetNode<Player>("Player");
            Player savedPlayer = Global.Instance.GetPlayer();
            GD.Print(savedPlayer.size);
            //playerNodeObj = savedPlayer;
            playerNodeObj.SetCameraLimits((int)dna.cameraLimits[0], (int)dna.cameraLimits[1], (int)dna.cameraLimits[2], (int)dna.cameraLimits[3]);
            playerNodeObj.Teleport(dna.playerSpawn);
            //GD.Print(playerNodeObj.Position);
        }

        public void OnDygnflowerHit()
        {
            int nonSleepingCount = 0;
            for (int i = 0; i < flowerObjects.Count; i++)
            {
                if (flowerObjects[i].status != DygnflowerStatus.Sleeping)
                {
                    nonSleepingCount++;
                }
            }
            if (nonSleepingCount == 2)
            {
                for (int i = 0; i < flowerObjects.Count; i++)
                {
                    if (flowerObjects[i].status != DygnflowerStatus.Sleeping)
                    {
                        flowerObjects[i].SetStatus(DygnflowerStatus.Active);
                    }
                }
            }
            FlowerLibrary.Instance.UpdateLibrary(1, flowerObjects);
        }

        public void OnDygnflowerTeleportChange(Vector2 newTeleportDestination)
        {
            if (newTeleportDestination != new Vector2(-9999999, -9999999))
            {
                currentTeleportDestinations.Add(newTeleportDestination);
                flowersInProximity++;
            }
            else
            {
                currentTeleportDestinations.Add(null);
                flowersInProximity--;
            }
            GD.Print(flowersInProximity);
        }

        public void OnPlayerInteract()
        {

            //for (int i = 0; i < currentTeleportDestinations.Count; i++)
            //{
            //    if (currentTeleportDestinations[i] is not null && flowersInProximity > 0)
            //    {
            //        Vector2 destination = currentTeleportDestinations[i].Value;
            //        currentTeleportDestinations.Clear();
            //        playerNodeObj.Teleport(destination);
            //        break;
            //    }
            //}

            for (int i = currentTeleportDestinations.Count - 1; i >= 0; i--)
            {
                if (currentTeleportDestinations[i] is not null && flowersInProximity > 0)
                {
                    Vector2 destination = currentTeleportDestinations[i].Value;
                    currentTeleportDestinations.Clear();
                    playerNodeObj.Teleport(destination);
                    break;
                }
            }
        }

        public void OnRoomBridgeEntered()
        {
            nextRoomDestination = Global.Instance.GetNextDestinationScene();
        }
    }
}
