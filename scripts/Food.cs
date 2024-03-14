using Godot;
using System;

public partial class Food : InventoryItem
{
	// Called when the node enters the scene tree for the first time.
	private int giveHealth;
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void setPlayer(Player p){
		player = p;
	}

	public void setGiveHealth(int i) {
		giveHealth = i;
	}

	public int getGiveHealth() {
		return giveHealth;
	}

}
