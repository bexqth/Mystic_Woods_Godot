using Godot;
using System;

public partial class AxeTool : InventoryItem
{

	// Called when the node enters the scene tree for the first time.
	private World world;
	public override void _Ready()
	{
		base._Ready();
		//world = (World)GetNode("/root/World");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void useItem() {
		if (player.getCanAxe() && !player.getAxing()) {
			player.setAxing(true);
			player.setCanAxe(false);
			//GD.Print(world);
			//this.world.setPlayerUsingAxe(true);
			player.getAxeCoolDownTimer().Start();
		}
	}

}
