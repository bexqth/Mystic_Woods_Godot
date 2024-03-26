using Godot;
using System;

public partial class Seed : InventoryItem
{
	[Export]
	public TileMap tileMap;
	private PackedScene plantScene;
	private PackedScene plantFoodScene;
	private Plant newPlant;
	private Food newPlantFood;
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
		base._Process(delta);
	}

	public void assignPlant(){
		switch(getItemName()) {
			case "CarrotSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/carrot_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Carrot");
				plantFoodScene = GD.Load<PackedScene>("res://scenes/carrot.tscn");
			break;

			case "PotatoSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/potato_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Potato");
				plantFoodScene = GD.Load<PackedScene>("res://scenes/potato.tscn");
			break;

			case "TurnipSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/turnip_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Turnip");
				plantFoodScene = GD.Load<PackedScene>("res://scenes/turnip.tscn");
			break;

			case "RadishSeed":
				plantScene = GD.Load<PackedScene>("res://scenes/radish_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Radish");
				plantFoodScene = GD.Load<PackedScene>("res://scenes/radish.tscn");
			break;
		}
	}

	public override void useItem() {
			Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
			TileData tileData = tileMap.GetCellTileData(0, tileMousePosition); //returns the tile
			
			if(tileData!= null) {

				if (player.getCanPlant() && !player.getPlanting()) {
					player.setPlanting(true);
					player.setCanPlant(false);
					player.getPlantCoolDownTimer().Start();
					var canPlaceSeeds = tileData.GetCustomData("canPlaceSeeds");
					if((bool)canPlaceSeeds) {
						assignPlant();
						newPlantFood = (Food)plantFoodScene.Instantiate();
						newPlant.setPlandFood(newPlantFood);
						newPlant.setPlayer(player);
						newPlantFood.setPlayer(player);
						Vector2 plantPixelPosition = new Vector2(tileMousePosition.X * 16 * 3 + 8*3 - 4, tileMousePosition.Y * 16 * 3 + 8*3 -2);
						//Vector2 plantPosition = new Vector2(clickedFarmTile.Position.X + 8, clickedFarmTile.Position.X + 8);

						newPlant.Position = plantPixelPosition;
						newPlantFood.Position = plantPixelPosition;
						player.GetParent().GetTree().CurrentScene.AddChild(newPlant);
						newPlant.grow();
					}
					else {
						GD.Print("cant place seeds here");
					}
				}
			}
	}
}
