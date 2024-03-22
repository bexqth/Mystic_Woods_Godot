using Godot;
using System;

public partial class EdibleItem : InventoryItem
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int addHealth;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public int getAddsHealth() {
		return this.addHealth;
	}

	public void addHealthToPlayer(Player player) {
		player.addHealth(this.addHealth);
	}

	public override void useItem() {
		
	}


}

