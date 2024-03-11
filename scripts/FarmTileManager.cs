using Godot;
using System;
using System.Collections.Generic;

public partial class FarmTileManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private List<FarmTile> farmTiles;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void addFarmTile(FarmTile newTile) {
		farmTiles.Add(newTile);
	}

	public void manageFarmTile() {

	}

	public FarmTile getClickedFarmTile(int x, int y) {
		for(int i = 0; i < farmTiles.Count; i++) {
			if(farmTiles[i].Position.X == x && farmTiles[i].Position.Y == y) {
				return farmTiles[i];
			} 
		}
		return null;
	}
}
