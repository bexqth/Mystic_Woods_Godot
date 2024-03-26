using Godot;
using System;

public partial class World : Node
{
	// Called when the node enters the scene tree for the first time.

	private InventoryItem draggedItem;
	private bool playerNearChest;
	private bool playerOpenedChest;
	private bool playerUsingCraftingTable;
	//private Crosshair crosshair;
	//private PackedScene crosshairScene;
	//private TileMap tileMap;
	public override void _Ready() {
		playerNearChest = false;
		playerOpenedChest = false;
		//GD.Print(tileMap);
		//crosshairScene = GD.Load<PackedScene>("res://scenes/crosshair.tscn");
		//crosshair = (Crosshair)crosshairScene.Instantiate();
		//AddChild(crosshair);
		//crosshair.setTileMap(GetNode<TileMap>("TileMap"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		//crosshair.followMouse();
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

	public void setPlayerOpenedChest(bool b) {
		playerOpenedChest = b;
	}

	public bool getPlayerOpenedChest() {
		return playerOpenedChest;
	}

	public void setPlayerUsingTable(bool b) {
		this.playerUsingCraftingTable = b;
	}

	public bool getPlayerUsingTable() {
		return this.playerUsingCraftingTable;
	}

}
