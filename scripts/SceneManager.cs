using Godot;
using System;
using System.Collections.Generic;

public partial class SceneManager : Node
{
	public static Player player{get;set;}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void changeScene(SceneChanger oldScene, string newScenePath)
	{
		//player = oldScene.player;
		//player.GetParent().RemoveChild(player);
		//oldScene.GetTree().ChangeSceneToFile(newScenePath);

	}

	
}
