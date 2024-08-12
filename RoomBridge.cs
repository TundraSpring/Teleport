using Godot;
using System;
using Teleport;

public partial class RoomBridge : Area2D
{
    public string nextDestinationScene;
    public string fullId;
    public int roomId;
    public int flowerAmo;
    public int rbAmo;
    public Vector4 cameraLimits;
    public Vector2 playerSpawn;

    [Signal]
    public delegate void EnteredEventHandler();

    public void SetData(string newDestinationScene, string newFullId, int newRoomId, int newFlowerAmo, int newRbAmo, Vector4 newCameraLimits, Vector2 newPlayerSpawn)
    {
        nextDestinationScene = newDestinationScene;
        fullId = newFullId;
        roomId = newRoomId;
        flowerAmo = newFlowerAmo;
        rbAmo = newRbAmo;
        cameraLimits = newCameraLimits;
        playerSpawn = newPlayerSpawn;
    }

    public void OnBodyEntered(Node2D node2D)
    {
        if (node2D.Name == "PlayerBody")
        {
            Global.Instance.SetNextDestination(this);
            EmitSignal(SignalName.Entered);
        }
    }
}
