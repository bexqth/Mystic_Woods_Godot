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
	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		text = GetNode<Label>("Label");
		animator.Play("closed");
		chestInventoryScene = GD.Load<PackedScene>("res://scenes/chest_inventory.tscn");
		chestInventory = (ChestInventory)chestInventoryScene.Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}


	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body.Name == "Player") {
			GD.Print("Chest should open");
			text.Text = "Press E to show chest inventoy";
			animator.Play("opening");
		}

	}

	private void _on_area_2d_body_exited(Node2D body)
	{
		if(body.Name == "Player") {
			GD.Print("Chest should close");
			clearText();
			animator.Play("closing");
		}
	}

	public void clearText() {
		text.Text = "";
	}
}

