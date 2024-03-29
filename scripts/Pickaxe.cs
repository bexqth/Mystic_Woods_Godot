using Godot;
using System;

public partial class Pickaxe : InventoryItem
{
	private int axeDamage = 20;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

    public override void useItem()
    {
        if (player.getCanPickaxe() && !player.getPickaxing()) {
			player.setPickaxing(true);
			player.setCanPickaxe(false);		
			player.getPickaxeCoolDownTimer().Start();
		}
    }

	public int getDamage() {
		return this.axeDamage;
	}

	
}
