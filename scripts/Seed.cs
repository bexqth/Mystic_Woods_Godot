using Godot;
using System;

public partial class Seed : InventoryItem
{
	[Export]
	public TileMap tileMap;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

    public override void useItem() {
		Vector2I tile = new Vector2I(2,1);
        Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		TileData tileData = tileMap.GetCellTileData(0, tileMousePosition); //returns the tile
		if(tileData != null) {
			var canPlaceSeeds = tileData.GetCustomData("canPlaceSeeds");
			if((bool)canPlaceSeeds) {
				//tileMap.SetCell(3, tileMousePosition, 8, tile); //why to place tile there and not create object??
				
			} else {
				GD.Print("cant place plats here");
			}
		}
    }
}
