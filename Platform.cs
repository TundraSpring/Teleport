using Godot;
using System;

public partial class Platform : Area2D
{
    public int timer = -1;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (timer != -1)
        {
            timer--;
            if (timer == 0)
            {
                QueueFree();
            }
        }
    }

	public void OnBodyEntered(Node2D node2d)
	{
		if (node2d.Name == "Projectile")
		{
            timer = 5;
        }
		
	}
}
