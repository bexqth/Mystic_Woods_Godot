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

	}

	public void assignPlant(){
		GD.Print(getItemName());
		switch(getItemName()) {
			case "CarrotSeed":
				//tile.SetCustomData("hasPlantName", "Carrot");
				plantScene = GD.Load<PackedScene>("res://scenes/carrot_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Carrot");

				plantFoodScene = GD.Load<PackedScene>("res://scenes/carrot.tscn");
				//clickedFarmTile.setPlant(newPlant);
			break;

			case "PotatoSeed":
				//tile.SetCustomData("hasPlantName", "Potato");
				plantScene = GD.Load<PackedScene>("res://scenes/potato_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Potato");

				plantFoodScene = GD.Load<PackedScene>("res://scenes/potato.tscn");
				//clickedFarmTile.setPlant(newPlant);
			break;

			case "TurnipSeed":
				//tile.SetCustomData("hasPlantName", "Turnip");
				plantScene = GD.Load<PackedScene>("res://scenes/turnip_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Turnip");

				plantFoodScene = GD.Load<PackedScene>("res://scenes/turnip.tscn");
				//clickedFarmTile.setPlant(newPlant);
			break;

			case "RadishSeed":
				//tile.SetCustomData("hasPlantName", "Radish");
				plantScene = GD.Load<PackedScene>("res://scenes/radish_grow.tscn");
				newPlant = (Plant)plantScene.Instantiate();
				newPlant.setPlantName("Radish");

				plantFoodScene = GD.Load<PackedScene>("res://scenes/radish.tscn");
				//clickedFarmTile.setPlant(newPlant);
			break;
		}
	}

	public override void useItem() {
		//if(clickedFarmTile != null) {
			assignPlant();
			newPlantFood = (Food)plantFoodScene.Instantiate();
			newPlant.setPlandFood(newPlantFood);
			newPlant.setPlayer(player);
			newPlantFood.setPlayer(player);
			Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
			Vector2 plantPixelPosition = new Vector2(tileMousePosition.X * 16 * 3 + 8*3, tileMousePosition.Y * 16 * 3 + 8*3);
			//Vector2 plantPosition = new Vector2(clickedFarmTile.Position.X + 8, clickedFarmTile.Position.X + 8);

			newPlant.Position = plantPixelPosition;
			newPlantFood.Position = plantPixelPosition;
			player.GetParent().GetTree().CurrentScene.AddChild(newPlant);
			newPlant.grow();

		//} else {
			//GD.Print("u cant place plats here");
		//}

	}
	
}

