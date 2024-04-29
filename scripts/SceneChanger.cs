using Godot;
using System;

public partial class SceneChanger : Node
{
	bool playerEntered;
	Label label;
	Player player;
	[Export]
	SceneChanger newSpawnPlace;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.playerEntered = false;
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
			this.player.Position = this.newSpawnPlace.GetNode<Area2D>("Area2D").GlobalPosition;
		}
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body is Player player) {
			playerEntered = true;
			this.player = player;
			this.label.Text = "Press E to enter";
		}
	}

	private void _on_area_2d_body_exited(Node2D body)
	{
		if(body is Player) {
			playerEntered = false;
			this.label.Text = "";
		}
	}
}
