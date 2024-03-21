using Godot;
using System;

public partial class Chest : Node2D
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	public Player player;
	private AnimatedSprite2D animator;
	private Label text;
	private PackedScene chestInventoryScene;
	private ChestInventory chestInventory;
	///private PackedScene chestInventoryScene;
	//private Inventory chestInventory;
	private bool chestInventoryIsVisible;
	private bool canOpenChest;
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

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		handleChest();
	}


	private void _on_area_2d_body_entered(Node2D body)
	{
		World world = (World)GetNode("/root/World");
		if(body.Name == "Player") {
			text.Text = "Press E to show chest inventoy";
			animator.Play("opening");
			canOpenChest = true;
			world.setPlayerNearChest(true);
		}

	}

	private void _on_area_2d_body_exited(Node2D body)
	{
		World world = (World)GetNode("/root/World");
		if(body.Name == "Player") {
			clearText();
			animator.Play("closing");
			canOpenChest = false;
			if(chestInventoryIsVisible) {
				chestInventory.Visible = false;
				chestInventoryIsVisible = false;
			}
			world.setPlayerNearChest(false);
		}
	}

	public void handleChest() {
		if(Input.IsActionJustPressed("pressed_e") && !chestInventoryIsVisible && canOpenChest) {
			GD.Print("Visible");
			chestInventory.Visible = true;
			chestInventoryIsVisible = true;
		} else if(Input.IsActionJustPressed("pressed_e") && chestInventoryIsVisible) {
			GD.Print("Hidden");
			chestInventory.Visible = false;
			chestInventoryIsVisible = false;
		}
	}

	public void clearText() {
		text.Text = "";
	}
}

