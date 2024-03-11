using Godot;
using System;

public partial class GardenPotTool : InventoryItem
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public TileMap tileMap;
	private int rangeOption = 1;

	public override void _Ready() {
		base._Ready();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public override void useItem() {

		int sourceId = 6;

		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		//Vector2I tilePlayerPosition = tileMap.LocalToMap(player.GlobalPosition);
		Vector2I tile = new Vector2I(2,0);

		//GD.Print("tile mouse pos " + tileMousePosition);
		//GD.Print("playes pos" + (int)player.GlobalPosition.X/48 + " " + (int)player.GlobalPosition.Y/48);
		//GD.Print("tile playes pos " + tilePlayerPosition);

		if(Math.Abs((int)player.GlobalPosition.X/48 - tileMousePosition.X) < 2 && Math.Abs((int)player.GlobalPosition.Y/48 - tileMousePosition.Y) < 2) {
			tileMap.SetCell(0, tileMousePosition, sourceId, tile);
		} else {
			GD.Print("out of reach");
		}
	}		


	public void showAllowedOption() {
		int sourceId = 6;
		Vector2I tile = new Vector2I(1,0);

		Vector2 playerPosition = player.Position;
		Vector2I tilePlayerPosition = tileMap.LocalToMap(playerPosition);

		Vector2I upTile = tilePlayerPosition + new Vector2I(0, -rangeOption);
		Vector2I downTile = tilePlayerPosition + new Vector2I(0, rangeOption);
		Vector2I leftTile = tilePlayerPosition + new Vector2I(-rangeOption, 0);
		Vector2I rightTile = tilePlayerPosition + new Vector2I(rangeOption, 0);

		tileMap.SetCell(0, upTile, sourceId, tile);
		tileMap.SetCell(0, downTile, sourceId, tile);
		tileMap.SetCell(0, leftTile, sourceId, tile);
		tileMap.SetCell(0, rightTile, sourceId, tile);
	}

}
