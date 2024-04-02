using Godot;
using System;

public partial class ResourceNodePickaxble : ResourceNode
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.assignResourceItem();
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public void assignResourceItem() {
		switch(this.Name) {
			case "coal_ore":
				this.resourceItemScene = GD.Load<PackedScene>("res://scenes/coal.tscn");
			break;

			case "iron_ore":
				this.resourceItemScene = GD.Load<PackedScene>("res://scenes/iron_rock.tscn");
			break;

			case "stone_ore":
				this.resourceItemScene = GD.Load<PackedScene>("res://scenes/stone.tscn");
			break;
		}

		this.resourceItem = (ResourceItem)resourceItemScene.Instantiate();
		this.setresourceItem(this.resourceItem);
		this.setresourceItemScene(this.resourceItemScene);
	}

	public override void dealDamage() {
		if(dealtDamage) {
			if(player.pickaxeAnimationEnd()) {
				reduceLife(recievedDamage);
				GD.Print("life is " + this.getLife());
				this.showHitSprite();
				spriteChangingTimer.Start();
				dealtDamage = false;
			}
		}
	}

	public override void setDamageValues() {
		if(player.getHoldingItem() is Pickaxe pickaxeTool) {
			dealtDamage = true;
			recievedDamage = pickaxeTool.getDamage();
		}
	}

	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if(Input.IsActionJustPressed("on_left_click") && this.canBeHit && player.getCanPickaxe()) {
			setDamageValues();
		}
	}

}
