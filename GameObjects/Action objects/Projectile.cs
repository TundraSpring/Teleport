using Godot;
using System;
using System.Collections.Generic;
using Teleport;

public partial class Projectile : CharacterBody2D
{
	[Export]
	int speed = 100;

	public float dir;
	public Vector2 spawnPos;
	public float spawnRot;

	Node owner;
	int team;
	List<int> canAttack;
	int damage;
	int damageType;
	double healAmo;
	bool canStagger;
	int lifeTimer = -1;

    public override void _Ready()
	{
		//GlobalPosition = spawnPos;
		//GlobalRotation = spawnRot;
		//timer = 5;
    }

	public override void _PhysicsProcess(double delta)
	{
		//Velocity = new Vector2(0, -speed).Rotated(dir);
		//MoveAndSlide();

		if (lifeTimer != -1)
		{
			lifeTimer--;
			if (lifeTimer == 0)
			{
				QueueFree();
			}
		}
	}

	public void SetData(Node owner, int team, List<int> canAttack, int damage, int damageType, double healAmo, bool canStagger, int lifeTimer)
	{
		this.owner = owner;
		this.team = team;
		this.canAttack = canAttack;
		this.damage = damage;
		this.damageType = damageType;
		this.healAmo = healAmo;
		this.canStagger = canStagger;
		this.lifeTimer = lifeTimer;
	}

	public void OnProjectileHit(Node2D node2d)
	{
		//GD.Print("Projectile hit: ", node2d.Name);
		//if (node2d == this)
		//{
		//	GD.Print("Hit itself");
		//}

        try
        {
            Player player = Global.Instance.GetPlayer();
            Global.Instance.GetCurrentRoom().RoomObjectEvent(player, this, ObjectEvent.AttackHit, node2d, node2d);
        }
        catch
        {
            GD.Print("ERROR: Projectile tried to send signal to non-existent room");
        }

    }


    public void Boo()
	{
		GD.Print("Boo!");
	}
}
