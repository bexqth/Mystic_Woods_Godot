using Godot;
using System;

public partial class ResourceNode : Node2D
{
	// Called when the node enters the scene tree for the first time.
	protected ResourceItem resourceItem;
	protected PackedScene resourceItemScene;
	protected int resourceItemCount;
	protected int life = 100;
	protected bool canBeHit;
	protected bool dealtDamage;
	protected int recievedDamage;
	protected Player player;
	protected Timer spriteChangingTimer;
	protected Sprite2D defaultSprite;
	protected Sprite2D focusedSprite;
	protected Sprite2D hitSprite;
	protected bool mouseEntered;
	public override void _Ready()
	{
		this.defaultSprite = GetNode<Sprite2D>("Sprite2D_default");
		this.focusedSprite = GetNode<Sprite2D>("Sprite2D_focused");
		this.hitSprite = GetNode<Sprite2D>("Sprite2D_hit");
		this.spriteChangingTimer = GetNode<Timer>("sprite_changing_Timer");
		this.spriteChangingTimer.WaitTime = 0.1;
		this.showDefaultSprite();
		this.mouseEntered = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		dealDamage();
		spawnResourceItem();
	}

	public void spawnResourceItem() {
		if(this.life <= 0) {
			//GD.Print(resourceItem);
			resourceItem.Position = this.Position;
			GetTree().CurrentScene.AddChild(resourceItem);
			this.QueueFree();
		}
	}

	public void setresourceItem(ResourceItem item) {
		this.resourceItem = item;
	}

	public void setresourceItemScene(PackedScene scene) {
		this.resourceItemScene = scene;
	}

	public void reduceLife(int i) {
		this.life -= i;
	}

	public int getLife() {
		return this.life;
	}
	public virtual void dealDamage() {

	}

	public virtual void setDamageValues() {

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
