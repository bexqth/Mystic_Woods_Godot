using Godot;
using System;

public partial class FarmTile : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private bool hasPlacedSeed;
	private String plantName;
		
	private Plant plant;
	private Food food;
	private FarmTileManager farmTileManager;
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
	}

	public void setFood(Food f) {
		food = f;
	}
	public Food getFood() {
		return food;
	}

	public void setPlant(Plant p) {
		plant = p;
	}

	public Plant getPlant() {
		return plant;
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
