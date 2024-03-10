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
				//Plant plant = new Plant();
				PackedScene plantScene = GD.Load<PackedScene>("res://scenes/carrot_grow.tscn");
				Plant plant = (Plant)plantScene.Instantiate();
				Vector2 plantPixelPosition = new Vector2(tileMousePosition.X * 16 * 3 + 8, tileMousePosition.Y * 16 * 3 + 8);
				plant.Position = plantPixelPosition;
				player.GetTree().CurrentScene.AddChild(plant);
				GD.Print("Tile position is " + tileMousePosition.X + " , " + tileMousePosition.Y);
				GD.Print("Plant position is " + plant.Position.X + " , " + plant.Position.Y);
				plant.grow();
				GD.Print("U planted a carrot");
			} else {
				GD.Print("cant place plats here");
			}
		}
	}
}
