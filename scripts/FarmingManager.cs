using Godot;
using System;
using System.Collections.Generic;

public partial class FarmingManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private List<FarmTile> farmTiles;
	private Player player;
	private TileMap tileMap;
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		farmTiles = new List<FarmTile>();
	}

	public void setPlayer(Player player) {
		this.player = player;
	}

	public void setTileMap(TileMap tileMap) {
		this.tileMap = tileMap;
	}

	public void addFarmTile(FarmTile newTile) {
		farmTiles.Add(newTile);
	}

	public void manageFarming() {

	}

	public FarmTile getClickedFarmTile() {
		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		
		for(int i = 0; i < farmTiles.Count; i++) {
			if(tileMousePosition.X == farmTiles[i].Position.X && tileMousePosition.Y == farmTiles[i].Position.Y) {
				return farmTiles[i];
			}
		}
		return null;

	}
}
