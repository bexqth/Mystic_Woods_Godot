using Godot;
using System;

public partial class CraftingTable : Node2D
{
	private PackedScene craftingUIScene;
	private CraftingUI craftingUI;
	private AnimatedSprite2D animator;
	private World world;
	private bool canUseTable;
	private bool isBeingUsed;
	private bool craftingUIVisible;
	private Label text;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animator.Play("notUsing");
		craftingUIScene = GD.Load<PackedScene>("res://scenes/crafting_ui.tscn");
		craftingUI = (CraftingUI)craftingUIScene.Instantiate();
		AddChild(craftingUI);
		text = GetNode<Label>("Label");
		world = (World)GetNode("/root/World");
		craftingUIVisible = false;
		craftingUI.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		handleTable();
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body.Name == "Player") {
			text.Text = "Press E to use crafting table";
			animator.Play("using");
			canUseTable = true;
			//world.setPlayerNearChest(true);
		}
	}


	private void _on_area_2d_body_exited(Node2D body)
	{
		if(body.Name == "Player") {
			clearText();
			animator.Play("unUsing");
			canUseTable = false;
			if(craftingUIVisible) {
				craftingUI.Visible = false;
				craftingUIVisible = false;
			}
			//world.setPlayerNearChest(false);
		}
	}

	public void handleTable() {
		if(Input.IsActionJustPressed("pressed_e") && !isBeingUsed && canUseTable) {
			craftingUI.Visible = true;
			craftingUIVisible = true;
			isBeingUsed = true;
			world.setPlayerUsingTable(true);
		} else if(Input.IsActionJustPressed("pressed_e") && isBeingUsed) {
			craftingUI.Visible = false;
			craftingUIVisible = false;
			isBeingUsed = false;
			world.setPlayerUsingTable(false);
		}
	}

	public void clearText() {
		text.Text = "";
	}

}

