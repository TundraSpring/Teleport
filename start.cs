using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Teleport;

public partial class start : Node2D
{
    List<Dygnflower> flowerObjects = new List<Dygnflower>();
	List<RoomBridge> roomBridges = new List<RoomBridge>();
	List<Vector2?> currentTeleportDestinations = new List<Vector2?>();
	string nextRoomDestination = "";
	Player player;
	int flowersInProximity = 0;
	


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        List<Dygnflower> flowers = FlowerLibrary.Instance.GetRoomData(1);
        Dygnflower flowerObj0 = GetNode<Dygnflower>("Nightflower1");
        Dygnflower flowerObj1 = GetNode<Dygnflower>("Nightflower2");
        flowerObj0.SetData(flowers[0].id, flowers[0].channel, flowers[0].status, flowers[0].type, flowers[0].teleportDestination, true);
		flowerObj1.SetData(flowers[1].id, flowers[1].channel, flowers[1].status, flowers[1].type, flowers[1].teleportDestination, true);
        flowerObjects.Add(flowerObj0);
		flowerObjects.Add(flowerObj1);

        List<RoomBridge> bridges = RbLibrary.Instance.GetRoomData(1);
        RoomBridge roomBridge = GetNode<RoomBridge>("RoomBridge1");
		roomBridge.SetData(bridges[0].fullId, bridges[0].roomId, bridges[0].destinationId, bridges[0].destinationEntrance);
		roomBridges.Add(roomBridge);


        player = GetNode<Player>("Player");
        //player.SetCameraLimits(-7627, -5000, 1260, 250);
        player.SetCameraLimits(-10000, -1600, 1260, 250);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (nextRoomDestination != "")
		{
            GetTree().ChangeSceneToFile(nextRoomDestination);
        }
    }

	public void OnNightFlowerHit()
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

	public void OnNightflowerTeleportChange(Vector2 newTeleportDestination)
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

        for (int i = 0; i < currentTeleportDestinations.Count; i++)
		{
			if (currentTeleportDestinations[i] is not null && flowersInProximity > 0)
			{
				Vector2 destination = currentTeleportDestinations[i].Value;
				currentTeleportDestinations.Clear();
                player.Teleport(destination);
                break;
            }
		}

        for (int i = 0; i < currentTeleportDestinations.Count; i++)
        {
            if (currentTeleportDestinations[i] is not null && flowersInProximity > 0)
            {
                Vector2 destination = currentTeleportDestinations[i].Value;
                currentTeleportDestinations.Clear();
                player.Teleport(destination);
                break;
            }
        }
    }

	public void OnRoomBridgeEntered()
	{
		nextRoomDestination = Global.Instance.GetNextRoomDestination();
    }
}
