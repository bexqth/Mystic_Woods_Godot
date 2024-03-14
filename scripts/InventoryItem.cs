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
		icon = GetNode<Sprite2D>("Sprite2D").Texture;
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
			player.addItemToInventory(this);
		}
	}

	public String getItemName() {
		return this.itemName;
	}

	public virtual void useItem() {
		
	}

	public void setPositionAfterDeletingFromItem() {
		Vector2 position = Position;
		position.X = player.Position.X;
		position.Y = player.Position.Y - 30;
		Position = position;
	}

	public bool getStackable()
	{
		return stackable;
	}
}
