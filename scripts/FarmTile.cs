using Godot;
using System;

public partial class FarmTile : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private bool hasPlacedSeed;
	private String plantName;

	[Signal]
	public delegate void FarmTileClickedEventHandler(FarmTile farmTile);
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void setPlacedSeed(bool b) {
		hasPlacedSeed = b;
	}

	public bool getPlacedSeed() {
		return hasPlacedSeed;
	}

	public void setPlantName(String s) {
		plantName = s;
	}

	public String getPlantName() {
		return plantName;
	}
}
