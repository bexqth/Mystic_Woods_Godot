using Godot;
using System;

public partial class Tool : InventoryItem
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int reduceTiredness;
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
