using System;
using System.Numerics;
using Godot;

public partial class Crosshair : Node2D
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	public TileMap tileMap;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.followMouse();
	}

	public void followMouse() {	
		Godot.Vector2 mousePosition = GetGlobalMousePosition() - tileMap.Position;
		int mouseX = (int)(mousePosition.X / 3);
		int mouseY = (int)(mousePosition.Y / 3);
		Godot.Vector2 newMousePosition = new Godot.Vector2(mouseX, mouseY);
		Vector2I tileMousePosition = tileMap.LocalToMap(newMousePosition);
		GD.Print(tileMousePosition);
		// Convert tile coordinates to pixel position and adjust for center
		Godot.Vector2 tilePixelPosition = new Godot.Vector2(tileMousePosition.X * 48 + 16, tileMousePosition.Y * 48 + 16);
		Position = tilePixelPosition;
	}

}
