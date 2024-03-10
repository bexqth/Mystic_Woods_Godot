using Godot;
using System;

public partial class Plant : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animator;
	private bool canBeDigged;
	
	public override void _Ready() {
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		canBeDigged = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		checkForGrowing();
	}

	public void grow() {
		animator.Play("grow");
		GD.Print("is growing");
	}

	public void checkForGrowing() {
		if(animator.Frame == 2) {
			canBeDigged = true;
		}
	}

	public bool getCanBeDigged(){
		return canBeDigged;
	}

	public void setCanBeDigged(bool b) {
		canBeDigged = b;
	}
}
