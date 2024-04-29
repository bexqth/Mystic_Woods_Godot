using Godot;
using System;

public partial class PlacedItem : InventoryItem
{

	private bool placed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.placed = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
