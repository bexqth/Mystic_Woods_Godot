using Godot;
using System;

public partial class Plant : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animator;
	
	public override void _Ready() {
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		
	}

	public void grow() {
		animator.Play("grow");
		GD.Print("is growing");
	}
}
