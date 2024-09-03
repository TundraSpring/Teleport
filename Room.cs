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
        public Player playerNodeObj;


        public override void _Ready()
        {
            GD.Print("fullId is now needed");
            RoomPacket rp = Global.Instance.GetRoomPacket(fullId);
            int spawnPos = Global.Instance.GetNewRoomSpawnPos();

            SetPlayer(rp, spawnPos);
            SetFlowers(rp.dygnflowers);
            SetRbs(rp.rbs);
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




        public void SetFlowers(List<Dygnflower> dygnflowers)
        {
            for (int i = 0; i < dygnflowers.Count; i++)
            {
                Dygnflower flowerNodeObj = GetNode<Dygnflower>($"Dygnflower{i}");
                flowerNodeObj.SetData(dygnflowers[i].id, dygnflowers[i].channel, dygnflowers[i].status, dygnflowers[i].type, dygnflowers[i].teleportDestination, true);
                flowerObjects.Add(flowerNodeObj);
            }
        }

        public void SetRbs(List<RoomBridge> rbs)
        {
            for (int i = 0; i < rbs.Count; i++)
            {
                RoomBridge rbObj = GetNode<RoomBridge>($"RoomBridge{i}");
                rbObj.SetData(rbs[i].nextDestinationScene, rbs[i].spawnPos);
                roomBridges.Add(rbObj);
            }
        }

        public void SetPlayer(RoomPacket rp, int spawnPos)
        {
            playerNodeObj = GetNode<Player>("Player");
            Player savedPlayer = Global.Instance.GetPlayer();
            GD.Print(savedPlayer.size);
            //playerNodeObj = savedPlayer;
            playerNodeObj.SetCameraLimits((int)rp.cameraLimits[0], (int)rp.cameraLimits[1], (int)rp.cameraLimits[2], (int)rp.cameraLimits[3]);
            playerNodeObj.Teleport(rp.playerSpawnPoints[spawnPos]);
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
            Global.Instance.UpdateRoomLibrary(fullId, flowerObjects);
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

        public void RoomObjectEvent(Node eventOwner, Node eventTrigger, ObjectEvent eventType, Node objectTriggered, Node affectedObject)
        {
            //Dygnflower becomes active
            if (eventType == ObjectEvent.HitboxEntered && objectTriggered is Dygnflower dfEntered)
            {
                if (dfEntered.status == DygnflowerStatus.Sleeping)
                {
                    dfEntered.SetStatus(DygnflowerStatus.Pending);
                    OnDygnflowerHit();
                }
                if (dfEntered.status == DygnflowerStatus.Active && eventTrigger.Name == "PlayerSoul")
                {
                    dfEntered.SetStatus(DygnflowerStatus.Ready);
                    currentTeleportDestinations.Add(dfEntered.teleportDestination);
                    flowersInProximity++;
                }
            }

            //Dygnflower becomes unreadied
            if (eventType == ObjectEvent.HitboxExited && objectTriggered is Dygnflower dfExited)
            {
                dfExited.SetStatus(DygnflowerStatus.Active);
                currentTeleportDestinations.Add(null);
                flowersInProximity--;
            }
        }
    }
}
