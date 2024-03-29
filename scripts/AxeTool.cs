using Godot;
using System;

public partial class AxeTool : InventoryItem
{

	//private World world;
	private int axeDamage = 20;
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void useItem() {
		if (player.getCanAxe() && !player.getAxing()) {
			player.setAxing(true);
			player.setCanAxe(false);		
			player.getAxeCoolDownTimer().Start();
		}
	}

	public int getDamage() {
		return this.axeDamage;
	}

}
