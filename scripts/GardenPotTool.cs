using Godot;
using System;

public partial class GardenPotTool : InventoryItem
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public TileMap tileMap;
	public override void _Ready() {
		base._Ready();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

    public override void useItem() {
		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		GD.Print("tile mouse position is " + tileMousePosition);
		int sourceId = 6;
		Vector2I tile = new Vector2I(2,0);
		tileMap.SetCell(0, tileMousePosition, sourceId, tile);
	}		


}
