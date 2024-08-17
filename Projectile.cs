using Godot;
using System;

public partial class Projectile : CharacterBody2D
{
	[Export]
	int speed = 100;

	public float dir;
	public Vector2 spawnPos;
	public float spawnRot;
	public int timer = -1;

	int owner;
	int team;
	int canAttack;
	int damage;
	int damageType;

    public override void _Ready()
	{
		//GlobalPosition = spawnPos;
		//GlobalRotation = spawnRot;
		timer = 2;
    }

	public override void _PhysicsProcess(double delta)
	{
		//Velocity = new Vector2(0, -speed).Rotated(dir);
		//MoveAndSlide();

		if (timer != -1)
		{
			timer--;
			if (timer == 0)
			{
				QueueFree();
			}
		}
	}

	public void OnProjectileHit(Node2D node2d)
	{
		GD.Print("Projectile hit: ", node2d.Name);
		if (node2d == this)
		{
			GD.Print("Hit itself");
		}
	}


    public void Boo()
	{
		GD.Print("Boo!");
	}
}
