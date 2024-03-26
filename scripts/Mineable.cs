using Godot;
using System;

public partial class Mineable : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Node2D resource;
	private int life;
	public override void _Ready()
	{
		life = 100;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void putDownRecourse() {
		if(life <= 0) {
			GetTree().CurrentScene.AddChild(resource);
		}
	}
}
