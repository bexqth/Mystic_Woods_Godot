using Godot;
using System;

public partial class Tree : ResourceNode
{
	World world;
	private ResourceItem resourceItem;
	private PackedScene resourceItemScene;
	private int resourceItemCount;
	private bool canBeHit;
	private bool dealtDamage;
	private int recievedDamage;
	private Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.resourceItemScene = GD.Load<PackedScene>("res://scenes/log.tscn");
		this.resourceItem = (Log)resourceItemScene.Instantiate();
		this.setresourceItem(this.resourceItem);
		this.setresourceItemScene(this.resourceItemScene);
		world = (World)GetNode("/root/World");
		//GD.Print("tree - " + world);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.PrintT("using axe " + world.getPlayerUsingAxe());
		//GD.Print(world.getPlayerUsingAxe());
		base._Process(delta);
		dealDamage();
	}

	public void dealDamage() {
		if(dealtDamage) {
			if(player.axeAnimationEnd()) {
				reduceLife(recievedDamage);
				GD.Print("life is " + this.getLife());
				dealtDamage = false;
			}
		}
	}

	public void setDamageValues() {
		if(player.getHoldingItem() is AxeTool axeTool) {
			dealtDamage = true;
			recievedDamage = axeTool.getDamage();
		}
	}

	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if(Input.IsActionJustPressed("on_left_click") && this.canBeHit && player.getCanAxe()) {
			setDamageValues();
		}
	}
	
	private void _on_area_2d_range_body_entered(Node2D body)
	{
		if(body is Player player) {
			GD.Print("Player in the range");
			this.player = player;
			canBeHit = true;
		}
	}	
	
	private void _on_area_2d_range_body_exited(Node2D body)
	{
		if(body is Player player) {
			GD.Print("Player out of the range");
			canBeHit = false;
			this.player = null;
		}
	}


}

