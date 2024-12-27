using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Teleport;

public partial class start : Room
{

    public override void _Ready()
    {
        fullId = "A01-R01";
        GD.Print("fullId is now set1");
        base._Ready();
    }
}
