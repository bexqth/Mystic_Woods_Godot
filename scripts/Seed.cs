using Godot;
using System;

public partial class Seed : InventoryItem
{
	[Export]
	public TileMap tileMap;
	private PackedScene plantScene;
	private Plant newPlant;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready() {
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public void assignPlant(TileData tile){
		switch(getItemName()) {
			case "CarrotSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/carrot_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Carrot");
				//tile.SetCustomData("hasPlantName", "Carrot");
			break;

			case "PotatoSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/potato_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Potato");
				//tile.SetCustomData("hasPlantName", "Potato");
				
			break;

			case "TurnipSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/turnip_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Turnip");
				//tile.SetCustomData("hasPlantName", "Turnip");
			break;

			case "RadishSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/radish_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Radish");
				//tile.SetCustomData("hasPlantName", "Radish");
			break;
		}

	} 
	public override void useItem() {
		Vector2I tile = new Vector2I(2,1);
		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		TileData tileData = tileMap.GetCellTileData(0, tileMousePosition); //returns the tile
		if(tileData != null) {
			var canPlaceSeeds = tileData.GetCustomData("canPlaceSeeds");
			var hasPlacedSeed = tileData.GetCustomData("hasPlacedSeed");

			if((bool)canPlaceSeeds /*&& !(bool)hasPlacedSeed*/) {
				assignPlant(tileData);
				Plant plant = (Plant)plantScene.Instantiate();
				Vector2 plantPixelPosition = new Vector2(tileMousePosition.X * 16 * 3 + 8*3, tileMousePosition.Y * 16 * 3 + 8*3);
				plant.Position = plantPixelPosition;
				player.GetTree().CurrentScene.AddChild(plant);
				//GD.Print("Tile position is " + tileMousePosition.X + " , " + tileMousePosition.Y);
				//GD.Print("Plant position is " + plant.Position.X + " , " + plant.Position.Y);

				//tileData.SetCustomData("hasPlacedSeed", true);
				plant.grow();
			} else {
				GD.Print("cant place plats here");
			}
		}
	}
}

