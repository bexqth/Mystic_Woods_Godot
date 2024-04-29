using Godot;
using System;

public partial class House : SceneChanger
{
	// Called when the node enters the scene tree for the first time.
	bool playerEntered = false;
	
	string scenePath;
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	
}

