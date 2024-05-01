using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

public partial class GardenPotTool : InventoryItem
{
	// Called when the node enters the scene tree for the first time.
	private TileMap tileMap;
	private FarmingManager farmingManager;
	private int rangeOption = 1;

	public override void _Ready() {
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		base._Process(delta);
	}

	public void setFarmingManager(FarmingManager fm) {
		this.farmingManager = fm;
		GD.Print("the manager was added");
		GD.Print(farmingManager);
	}

	public override void useItem() {
		var world = World.Instance;
		this.tileMap = world.GetTileMap();

		if (player.getCanWater() && !player.getWatering()) {
			player.setWatering(true);
			player.setCanWater(false);
			player.getWaterCoolDownTimer().Start();
			int sourceId = 7;
			Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
			Vector2I tile = new Vector2I(2,0);

			if(Math.Abs((int)player.GlobalPosition.X/48 - tileMousePosition.X) < 2 && Math.Abs((int)player.GlobalPosition.Y/48 - tileMousePosition.Y) < 2) {
				tileMap.SetCell(0, tileMousePosition, sourceId, tile);
			} else {
				GD.Print("out of reach");
			}
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
