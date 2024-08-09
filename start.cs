using Godot;
using System;
using System.Collections.Generic;
using Teleport;

public partial class start : Node2D
{
    List<Dygnflower> flowerObjects = new List<Dygnflower>();
	List<Vector2?> currentTeleportDestinations = new List<Vector2?>();
	Player player;
	int flowersInProximity = 0;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Dygnflower flowerObj1 = GetNode<Dygnflower>("Nightflower1");
        Dygnflower flowerObj2 = GetNode<Dygnflower>("Nightflower2");
        flowerObj1.SetData("A01-R01-F01", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-9452, -16));
        flowerObj2.SetData("A01-R01-F02", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-1180, -99));
        flowerObjects.Add(flowerObj1);
		flowerObjects.Add(flowerObj2);

        player = GetNode<Player>("Player");
        //player.SetCameraLimits(-7627, -5000, 1260, 250);
        player.SetCameraLimits(-10000, -1600, 1260, 250);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        FlowerData.Instance.Health = 5;
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
		//GetTree().ChangeSceneToFile("res://area_2.tscn");

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
}
