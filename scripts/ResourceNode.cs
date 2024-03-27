using Godot;
using System;

public partial class ResourceNode : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private ResourceItem resourceItem;
	private PackedScene resourceItemScene;
	private int resourceItemCount;
	private int life = 100;
	World world;
	public override void _Ready()
	{
		world = (World)GetNode("/root/World");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		spawnResourceItem();
	}

	public World getWorld() {
		return this.world;
	}

	public void spawnResourceItem() {
		if(this.life <= 0) {
			GD.Print(resourceItem);
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
	
}
