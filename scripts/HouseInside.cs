using Godot;
using System;

public partial class HouseInside : Node
{
	// Called when the node enters the scene tree for the first time.
	bool playerEntered = false;
	Label label;
	public override void _Ready()
	{
		this.label = GetNode<Label>("Label");
		this.label.Text = "";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		checkInput();
	}

	public void checkInput() {
		if(Input.IsActionJustPressed("pressed_e") && playerEntered) {
			GetTree().ChangeSceneToFile("res://scenes/main.tscn");
		}
	}

	private void _on_exit_area_2d_body_entered(Node body)
	{
		if(body is Player) {
			playerEntered = true;
			this.label.Text = "Press E to exit";
		}
	}


	private void _on_exit_area_2d_body_exited(Node body)
	{
		if(body is Player) {
			playerEntered = false;
			this.label.Text = "";
		}
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		// Replace with function body.
	}


	private void _on_area_2d_body_exited(Node2D body)
	{
		// Replace with function body.
	}

}

