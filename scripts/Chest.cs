using Godot;
using System;

public partial class Chest : PlacableItem
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	public Player player;
	private AnimatedSprite2D animator;
	private Label text;
	private PackedScene chestInventoryScene;
	private ChestInventory chestInventory;
	private bool chestInventoryIsVisible;
	private bool canOpenChest;
	private bool isOpen;
	private World world;
	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		text = GetNode<Label>("Label");
		animator.Play("closed");
		chestInventoryScene = GD.Load<PackedScene>("res://scenes/chest_inventory.tscn");
		chestInventory = (ChestInventory)chestInventoryScene.Instantiate();
		AddChild(chestInventory);
		//chestInventory.Position = GetViewportRect().Size / 2;
		chestInventoryIsVisible = false;
		chestInventory.Visible = false;
		world = (World)GetNode("/root/World");
		base._Ready();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		handleChest();
		base._Process(delta);
	}


	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body.Name == "Player") {
			text.Text = "Press E to open chest";
			animator.Play("opening");
			canOpenChest = true;
			//world.setPlayerNearChest(true);
		}
	}
	

	private void _on_area_2d_body_exited(Node2D body)
	{
		if(body.Name == "Player") {
			clearText();
			animator.Play("closing");
			canOpenChest = false;
			if(chestInventoryIsVisible) {
				chestInventory.Visible = false;
				chestInventoryIsVisible = false;
			}
			//world.setPlayerNearChest(false);
		}
	}

	public void handleChest() {
		if(Input.IsActionJustPressed("pressed_e") && !isOpen && canOpenChest) {
			//GD.Print("Visible");
			chestInventory.Visible = true;
			chestInventoryIsVisible = true;
			isOpen = true;
			world.setPlayerOpenedChest(true);
		} else if(Input.IsActionJustPressed("pressed_e") && isOpen) {
			//GD.Print("Hidden");
			chestInventory.Visible = false;
			chestInventoryIsVisible = false;
			isOpen = false;
			world.setPlayerOpenedChest(false);
		}
	}

	public void clearText() {
		text.Text = "";
	}
}
