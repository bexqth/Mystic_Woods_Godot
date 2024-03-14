using Godot;
using System;

public partial class HoeTool : InventoryItem
{
	[Export]
	public TileMap tileMap;
	private PackedScene foodPlantScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void getPlantFood(TileData tile) {
		switch((string)tile.GetCustomData("hasPlantName")) {
			case "Carrot":
				GD.Print("tile name " + tile.GetCustomData("hasPlantName"));
				foodPlantScene = GD.Load<PackedScene>("res://scenes/carrot.tscn");
			break;

			case "Potato":
				GD.Print("tile name " + tile.GetCustomData("hasPlantName"));
				foodPlantScene = GD.Load<PackedScene>("res://scenes/potato.tscn");
			break;

			case "Turnip":
				GD.Print("tile name " + tile.GetCustomData("hasPlantName"));
				foodPlantScene = GD.Load<PackedScene>("res://scenes/turnip.tscn");
			break;

			case "Radish":
				GD.Print("tile name " + tile.GetCustomData("hasPlantName"));
				foodPlantScene = GD.Load<PackedScene>("res://scenes/radish.tscn");
			break;
		}
	}

    public override void useItem() {
		
		int sourceIdGrass = 1;
		Vector2I grassTile = new Vector2I(0,0);	
		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		tileMap.SetCell(0, tileMousePosition, sourceIdGrass, grassTile);
	
    }
}
