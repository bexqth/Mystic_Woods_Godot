using Godot;
using System;

public partial class Bed : Node
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animator;
	private Label label;
	private bool inBed;
	private Player player;
	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		label = GetNode<Label>("Label");
		this.inBed = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		handleBed();
	}

	public void handleBed() {
		if(player != null) {
			if(Input.IsActionJustPressed("pressed_b")) {
				if(!inBed) {
					this.player.Visible = false;
					this.setOcupiedBed();
				} else {
					this.player.Visible = true;
					this.setEmptyBed();
				}				
			}
		}

	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body is Player player) {
			if(!inBed) {
				label.Text = "Press B to sleep";
			}		
			this.player = player;
		}
	}

	public void setEmptyBed() {
		animator.Play("empty");
		this.inBed = false;
		this.player.CanMove = true;
		label.Text = "Press B to sleep";
	}

	public void setOcupiedBed() {
		animator.Play("falling_asleep");
		animator.Play("sleep");
		this.player.CanMove = false;
		label.Text = "Press B to get up";
		this.inBed = true;
	}

	private void _on_area_2d_body_exited(Node2D body)
	{
		if(body is Player) {
			label.Text = "";
			this.player = null;
		}
	}

}

