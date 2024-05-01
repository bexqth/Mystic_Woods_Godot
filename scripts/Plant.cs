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
	private TileMap tileMap;
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
		var world = World.Instance;
		this.tileMap = world.GetTileMap();
		int sourceIdGrass = 2;
		Vector2I grassTile = new Vector2I(1,1);
		Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
		GD.Print(player.getNameHoldingItem());
		if (Input.IsActionJustPressed("on_left_click") && player.getNameHoldingItem() == "hoe") {
			
			if(getCanBeDigged()) {

				if(Math.Abs((int)player.GlobalPosition.X/48 - tileMousePosition.X) < 2 && Math.Abs((int)player.GlobalPosition.Y/48 - tileMousePosition.Y) < 2) {
					tileMap.SetCell(0, tileMousePosition, sourceIdGrass, grassTile);
				} else{
					GD.Print("out of reach");
				}
				GD.Print(plantFood);
				player.GetParent().GetTree().CurrentScene.AddChild(plantFood);
				this.QueueFree();
			} else {
				GD.Print("The plant didnt grow fully");
				if(Math.Abs((int)player.GlobalPosition.X/48 - tileMousePosition.X) < 2 && Math.Abs((int)player.GlobalPosition.Y/48 - tileMousePosition.Y) < 2) {
					tileMap.SetCell(0, tileMousePosition, sourceIdGrass, grassTile);
				} else{
					GD.Print("out of reach");
				}
				this.QueueFree();
			}
			
		}
	}


	private void _on_area_2d_mouse_entered() {
		
	}
}
		
