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

	[Signal]
	public delegate void FarmTileClickedEventHandler(FarmTile clickedFarmTile); //creating signal

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

	public void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx) {
		if(Input.IsActionJustPressed("on_left_click")) {
			EmitSignal(nameof(FarmTileClickedEventHandler), this);
		}	
	}

}

