using Godot;
using System;

public partial class PlacableItem : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	float XPosition{get;set;}
	[Export]
	float YPosition{get;set;}
	protected bool isSelected;
	public bool CanBePlaced{get;set;}
	private TileMap tileMap;
	private PlacableItem collidedItem;
	public override void _Ready()
	{
		isSelected = false;
		CanBePlaced = true;
	}

	public override void _Process(double delta)
	{
		if(isSelected) {
			followMouse();
			//GD.Print(this.Name + " " + canBePlaced);
		}
	}

	public void followMouse() {
		
		var world = World.Instance;
		var x = world.getCrossHairPosition().X - this.XPosition;
		var y = world.getCrossHairPosition().Y - this.YPosition;
		Position = new Vector2(x,y);
	}


	private void _on_area_2d_collision_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		var world = World.Instance;

		if (Input.IsActionJustPressed("on_right_click")){
			isSelected = true;
			this.Modulate = new Color("ffffffbb");
			this.ZIndex = 5;
			world.PlacableItemPickedUp = true;
			world.PlacableItem = this;

		} else if(Input.IsActionJustPressed("on_left_click") && isSelected) {
			//GD.Print(canBePlaced);
			if(this.CanBePlaced == true) {				
				isSelected = false;
				this.Modulate = new Color("ffffff");
				this.ZIndex = 0;
				world.PlacableItemPickedUp = false;			
			} else {
				GD.Print("the item cant be placed here");
			}	
		}
	}
		
	private void _on_area_2d_collision_area_entered(Area2D area)
	{
		var world = World.Instance;
		if(area.GetParent() is PlacableItem) {
			if(world.PlacableItemPickedUp  && world.PlacableItem != this) {
				world.PlacableItem.Modulate = new Color("fa6269c8");
				world.PlacableItem.CanBePlaced = false;
				collidedItem = this;
				GD.Print(world.PlacableItem.Name + " colliding with " + collidedItem.Name);						
			}
		}
	}


	private void _on_area_2d_collision_area_exited(Area2D area)
	{
		var world = World.Instance;
		
		if(area.GetParent() is PlacableItem) {
			
			if(world.PlacableItemPickedUp && world.PlacableItem != this) {
				world.PlacableItem.Modulate = new Color("ffffff");
			
				world.PlacableItem.CanBePlaced = true;
				collidedItem = null;
				
				GD.Print(world.PlacableItem.Name + " colliding with null");
			}
		}
	}
	
}


