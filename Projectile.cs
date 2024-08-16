using Godot;
using System;

public partial class Projectile : CharacterBody2D
{
	[Export]
	int speed = 100;

	public float dir;
	public Vector2 spawnPos;
	public float spawnRot;

    public override void _Ready()
	{
		GlobalPosition = spawnPos;
		GlobalRotation = spawnRot;

    }

	public override void _PhysicsProcess(double delta)
	{
		//Velocity = new Vector2(0, -speed).Rotated(dir);
		//MoveAndSlide();
	}

	public void Boo()
	{
		GD.Print("Boo!");
	}
}
