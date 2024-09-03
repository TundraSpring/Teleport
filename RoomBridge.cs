using Godot;
using System;
using Teleport;

public partial class RoomBridge : Area2D
{
    public string nextDestinationScene;
    public int spawnPos;

    [Signal]
    public delegate void EnteredEventHandler();

    public void SetData(string nextDestinationScene, int spawnPos)
    {
        this.nextDestinationScene = nextDestinationScene;
        this.spawnPos = spawnPos;
    }

    public RoomBridge()
    {

    }

    public RoomBridge(string nextDestinationScene, int spawnPos)
    {
        this.nextDestinationScene = nextDestinationScene;
        this.spawnPos = spawnPos;
    }

    public void OnBodyEntered(Node2D node2D)
    {
        if (node2D.Name == "PlayerBody")
        {
            Global.Instance.SetNextDestination(this);
            EmitSignal(SignalName.Entered, "1", "2");
        }
    }
}
