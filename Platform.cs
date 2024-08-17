using Godot;
using System;

public partial class Platform : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D node2d)
	{
		GD.Print("Platform was hit by: ", node2d.Name);
	}
}
