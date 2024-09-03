using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomPacket
{
    public List<Dygnflower> dygnflowers;
    public List<RoomBridge> rbs;
    public Vector4 cameraLimits;
    public List<Vector2> playerSpawnPoints;

    public RoomPacket(List<Dygnflower> dygnflowers, List<RoomBridge> rbs, Vector4 cameraLimits, List<Vector2> playerSpawnPoints)
    {
        this.dygnflowers = dygnflowers;
        this.rbs = rbs;
        this.cameraLimits = cameraLimits;
        this.playerSpawnPoints = playerSpawnPoints;
    }

    public void SetData(List<Dygnflower> dygnflowers, List<RoomBridge> rbs)
    {
        this.dygnflowers = dygnflowers;
        this.rbs = rbs;
    }

    public void UpdateDygnflowers(List<Dygnflower> dygnflowers)
    {
        if (dygnflowers.Count == 0)
        {
            return;
        }
        this.dygnflowers = dygnflowers;
    }

    public void UpdateRBs(List<RoomBridge> RBs)
    {
        if (dygnflowers.Count == 0)
        {
            return;
        }
        this.rbs = RBs;
    }
}
