using Godot;
using System;

public partial class Plant : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animator;
	private bool canBeDigged;
	private int tileX;
	private int tileY;
	private String plantName;
	private Food plantFood;
	private int posX;
	private int posY;
	private Player player;
	public override void _Ready() {
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		canBeDigged = false;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		checkForGrowing();
	}

	public void setPlayer(Player p) {
		player = p;
	}

	public void setPlandFood(Food plantFood) {
		this.plantFood = plantFood;
	}

	public void setPlantName(String s) {
		plantName = s;
	}

	public String getPlantName() {
		return plantName;
	}

	public void grow() {
		animator.Play("grow");
	}

	public void checkForGrowing() {
		if(animator.Frame == 2) {
			canBeDigged = true;
		}
	}

	public bool getCanBeDigged(){
		return canBeDigged;
	}

	public void setX(int x) {
		posX = x;
	}

	public void setY(int y) {
		posY = y;
	}

	public void setCanBeDigged(bool b) {
		canBeDigged = b;
	}

	public void setTileX(int i){
		tileX = i;
	}

	public void setTileY(int i) {
		tileY = i;
	}


	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx) {
		if (Input.IsActionJustPressed("on_left_click") && player.getNameHoldingItem() == "hoe") {
			GD.Print("area 2d clicked");
			GD.Print(plantFood);
			this.QueueFree();
			player.GetParent().GetTree().CurrentScene.AddChild(plantFood);
		}
	}
}
