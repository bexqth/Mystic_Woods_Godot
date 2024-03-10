using Godot;
using System;

public partial class World : Node
{

	private TileMap tileMap;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		tileMap = GetNode<TileMap>("TileMap");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		//mouseControl();
	}

	/*public void mouseControl() {
		if(Input.IsActionJustPressed("on_left_click")) {
			var mousePosition = GetViewport().GetMousePosition();
			var tileMousePosition = tileMap.LocalToMap(mousePosition);
			int sourceId = 6;
			Vector2I tile = new Vector2I(2,0);
			tileMap.SetCell(0, tileMousePosition, sourceId, tile);
		}
	}*/
}
