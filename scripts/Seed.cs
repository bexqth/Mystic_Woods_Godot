using Godot;
using System;

public partial class Seed : InventoryItem
{
	[Export]
	public TileMap tileMap;
	private PackedScene plantScene;
	private Plant newPlant;
	//private FarmTile clickedFarmTile;
	private FarmTile clickedFarmTile;
	//private Vector2I plantTile;
	//private int plantTileIndex;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready() {
		base._Ready();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public void assignPlant(FarmTile clickedTile){
		switch(getItemName()) {
			case "CarrotSeed":
				//tile.SetCustomData("hasPlantName", "Carrot");
				plantScene = GD.Load<PackedScene>("res://scenes/carrot_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Carrot");
				clickedFarmTile.setPlant(newPlant);
			break;

			case "PotatoSeed":
				//tile.SetCustomData("hasPlantName", "Potato");
				plantScene = GD.Load<PackedScene>("res://scenes/potato_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Potato");
				clickedFarmTile.setPlant(newPlant);
			break;

			case "TurnipSeed":
				//tile.SetCustomData("hasPlantName", "Turnip");
				plantScene = GD.Load<PackedScene>("res://scenes/turnip_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Turnip");
				clickedFarmTile.setPlant(newPlant);
			break;

			case "RadishSeed":
				//tile.SetCustomData("hasPlantName", "Radish");
				plantScene = GD.Load<PackedScene>("res://scenes/radish_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Radish");
				clickedFarmTile.setPlant(newPlant);
			break;
		}

	}

	public void OnFarmTileClicked(FarmTile tile) {
		clickedFarmTile = tile;
	}

	public override void useItem() {
		if(clickedFarmTile != null) {
			assignPlant(clickedFarmTile);
			Plant plant = (Plant)plantScene.Instantiate();
			//Vector2 plantPixelPosition = new Vector2(tileMousePosition.X * 16 * 3 + 8*3, tileMousePosition.Y * 16 * 3 + 8*3);
			Vector2 plantPosition = new Vector2(clickedFarmTile.Position.X + 8, clickedFarmTile.Position.X + 8);

			plant.Position = plantPosition;
			//player.GetTree().CurrentScene.AddChild(plant);
			player.GetParent().GetTree().CurrentScene.AddChild(plant);
			plant.grow();

		} else {
			GD.Print("u cant place plats here");
		}

		/*Vector2I tile = new Vector2I(2,1);
		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		TileData tileData = tileMap.GetCellTileData(0, tileMousePosition); //returns the tile
		if(tileData != null) {
			var canPlaceSeeds = tileData.GetCustomData("canPlaceSeeds");

			if((bool)canPlaceSeeds) {
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
		}*/

	}
	
}

