using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class InventoryItem : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public String name;
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


}
