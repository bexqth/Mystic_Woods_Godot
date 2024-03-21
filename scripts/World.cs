using Godot;
using System;

public partial class World : Node
{
	// Called when the node enters the scene tree for the first time.

	private InventoryItem draggedItem;
	private bool playerNearChest;
	public override void _Ready() {
		playerNearChest = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public void setDraggedItem(InventoryItem dr) {
		draggedItem = dr;
	}

	public InventoryItem getDraggedItem() {
		return draggedItem;
	}

	public bool getPlayerNearChest() {
		return playerNearChest;
	}

	public void setPlayerNearChest(bool b) {
		playerNearChest = b;
	}



}
