using Godot;
using System;
using System.Collections.Generic;

public partial class InventorySlot : Button
{
	// Called when the node enters the scene tree for the first time.
	private List<InventoryItem> chestItems;
	public override void _Ready()
	{
		chestItems = new List<InventoryItem>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
