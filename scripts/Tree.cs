using Godot;
using System;

public partial class Tree : ResourceNode
{
	private ResourceItem resourceItem;
	private PackedScene resourceItemScene;
	private int resourceItemCount;
	private bool canBeHit;
	private bool dealtDamage;
	private int recievedDamage;
	private Player player;
	private Timer spriteChangingTimer;
	private Sprite2D defaultSprite;
	private Sprite2D focusedSprite;
	private Sprite2D hitSprite;
	private bool mouseEntered;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.resourceItemScene = GD.Load<PackedScene>("res://scenes/log.tscn");
		this.resourceItem = (Log)resourceItemScene.Instantiate();
		this.setresourceItem(this.resourceItem);
		this.setresourceItemScene(this.resourceItemScene);
		this.defaultSprite = GetNode<Sprite2D>("Sprite2D_default");
		this.focusedSprite = GetNode<Sprite2D>("Sprite2D_focused");
		this.hitSprite = GetNode<Sprite2D>("Sprite2D_hit");
		spriteChangingTimer = GetNode<Timer>("sprite_changing_Timer");
		this.showDefaultSprite();
		this.mouseEntered = false;
		//GD.Print("tree - " + world);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		dealDamage();
	}

	public void dealDamage() {
		if(dealtDamage) {
			if(player.axeAnimationEnd()) {
				reduceLife(recievedDamage);
				GD.Print("life is " + this.getLife());
				this.showHitSprite();
				spriteChangingTimer.Start();
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
		if(body is Player) {
			GD.Print("Player out of the range");
			canBeHit = false;
			this.player = null;
		}
	}


	public void showDefaultSprite() {
		this.defaultSprite.Show();
		this.focusedSprite.Hide();
		this.hitSprite.Hide();
	}

	public void showFocusedSprite() {
		this.defaultSprite.Hide();
		this.focusedSprite.Show();
		this.hitSprite.Hide();
	}
	public void showHitSprite() {
		this.defaultSprite.Hide();
		this.focusedSprite.Hide();
		this.hitSprite.Show();
	}

	private void _on_sprite_changing_timer_timeout()
	{
		if(mouseEntered) {
			this.showFocusedSprite();
		} else {
			this.showDefaultSprite();
		}
	}

	private void _on_area_2d_mouse_entered()
	{
		this.mouseEntered = true;
		this.showFocusedSprite();
	}

	private void _on_area_2d_mouse_exited()
	{
		this.mouseEntered = false;
		this.showDefaultSprite();
	}

}

