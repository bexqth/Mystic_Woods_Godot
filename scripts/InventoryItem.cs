using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

public partial class InventoryItem : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public String itemName;
	[Export] 
	public Texture2D icon;
	[Export] 
	public bool stackable;
	[Export] 
	public int index;
	[Export] 
	public bool isInInventory;
	[Export]
	public Player player;
	private int count;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Texture2D getIcon()
	{
		return this.icon;
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body.Name == "Player") {
			GD.Print(this.Name + " was added to the inventory");
			player.addItemToInventory(this);
			//QueueFree();
		}
	}

	public String getItemName() {
		return this.itemName;
	}

	public virtual void useItem() {}

	public void setPositionAfterDeletingFromItem() {
		Vector2 position = Position;
		GD.Print(position.X + " , " + position.Y);
		position.X = player.Position.X;
		position.Y = player.Position.Y - 20;
		GD.Print("item " + position.X + " , " + position.Y);
		GD.Print("player " + player.Position.X + " " + player.Position.Y);
		Position = position;
	}


}
