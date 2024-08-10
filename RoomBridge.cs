using Godot;
using System;
using Teleport;

public partial class RoomBridge : Area2D
{
    public string fullId;
    public int roomId;
    public string destinationId;
    public int destinationEntrance;

    [Signal]
    public delegate void EnteredEventHandler();

    public void SetData(string fullId, int roomId, string destinationId, int destinationEntrance)
    {
        this.fullId = fullId;
        this.roomId = roomId;
        this.destinationId = destinationId;
        this.destinationEntrance = destinationEntrance;
    }

    public void OnBodyEntered(Node2D node2D)
    {
        if (node2D.Name == "PlayerBody")
        {
            Global.Instance.SetNextDestination(destinationId, destinationEntrance);
            EmitSignal(SignalName.Entered);
        }
    }
}
