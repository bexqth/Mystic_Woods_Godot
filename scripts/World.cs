using Godot;
using System;

public partial class World : Node2D
{
	// Called when the node enters the scene tree for the first time.

	private InventoryItem draggedItem;
	private bool playerNearChest;
	private bool playerOpenedChest;
	private bool playerUsingCraftingTable;
	private bool playerUsingFurnace;
	private bool playerUsingAxe;
	private static Vector2 playerPosition;
	public static World Instance { get; private set; }
	private bool playerCanUseAxe;
	[Export]
	public TileMap tileMap;
	[Export]
	public Player player;
	public override void _Ready() {
		playerNearChest = false;
		playerOpenedChest = false;
		playerUsingAxe = false;
		playerCanUseAxe = false;
		playerUsingFurnace = false;
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		//crosshair.followMouse();
	}

	public  TileMap GetTileMap() {
		return tileMap;
	}

	public  Player getPlayer() {
		return player;
	}

	public static void setPlayerPosition(Vector2 vector2) {
		playerPosition = vector2;
	}

	public Vector2 getPlayerPosition() {
		return playerPosition;
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

	public bool getPlayerUsingFurnace() {
		return this.playerUsingFurnace;
	}

	public void setPlayerUsingFurnace(bool b) {
		this.playerUsingFurnace = b;
	}

	public bool getPlayerUsingAxe() {
		return this.playerUsingAxe;
	}

	public void setPlayerUsingAxe(bool b) {
		this.playerUsingAxe = b;
	}

}
