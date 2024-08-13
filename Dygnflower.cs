using Godot;
using System;
using Teleport;

public partial class Dygnflower : Area2D
{
	public string id;
	public int channel;
	public DygnflowerStatus status;
	public DygnflowerType type;
    public Vector2 teleportDestination;

    [Signal]
    public delegate void HitEventHandler();

    [Signal]
    public delegate void SetTeleportEventHandler();

    public void SetData(string id, int channel, DygnflowerStatus status, DygnflowerType? type, Vector2 teleportDestination, bool changeSprite)
	{
		this.id = id;
        this.channel = channel;
        if (type is not null)
        {
            this.type = (DygnflowerType)type;
        }
        if (changeSprite)
        {
            SetStatus(status);
        }
        else
        {
            this.status = status;
        }
        this.teleportDestination = teleportDestination;
    }


    public void SetStatus(DygnflowerStatus newStatus)
	{
        status = newStatus;
        AnimatedSprite2D sprite = GetNode<AnimatedSprite2D>("DygnflowerAnimation");
		if (newStatus == DygnflowerStatus.Sleeping)
		{
            if (type == DygnflowerType.Night)
			{
                sprite.Animation = "SleepingNight";
			}
			else
			{
                sprite.Animation = "SleepingDay";
            }
		}
		else if (newStatus == DygnflowerStatus.Pending)
		{
            if (type == DygnflowerType.Night)
            {
                sprite.Animation = "PendingNight";
            }
            else
            {
                sprite.Animation = "PendingDay";
            }
        }
        else if (newStatus == DygnflowerStatus.Active)
        {
            if (type == DygnflowerType.Night)
            {
                sprite.Animation = "ActiveNight";
            }
            else
            {
                sprite.Animation = "ActiveDay";
            }
        }
        else // "Ready"
        {
            if (type == DygnflowerType.Night)
            {
                sprite.Animation = "ReadyNight";
            }
            else
            {
                sprite.Animation = "ReadyDay";
            }
        }
    }

    public void OnBodyExited(Node2D node2D)
    {
        if (node2D.Name == "PlayerSoul")
        {
            if (status == DygnflowerStatus.Ready)
            {
                SetStatus(DygnflowerStatus.Active);
                EmitSignal(SignalName.SetTeleport, new Vector2(-9999999, -9999999));
            }
        }
    }

    public void OnBodyEntered(Node2D node2D)
    {
            if (status == DygnflowerStatus.Sleeping)
            {
                SetStatus(DygnflowerStatus.Pending);
                EmitSignal(SignalName.Hit);
            }
            if ((status == DygnflowerStatus.Active || status == DygnflowerStatus.Ready) && node2D.Name == "PlayerSoul")
            {
                SetStatus(DygnflowerStatus.Ready);
                EmitSignal(SignalName.SetTeleport, teleportDestination);
            }
    }
}
