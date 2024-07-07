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

	public override void _Ready()
	{
		isSelected = false;
	}

	public override void _Process(double delta)
	{
		if(isSelected) {
			followMouse();
		}
	}

	public void followMouse() {
		var x = GetGlobalMousePosition().X - this.XPosition;
		var y = GetGlobalMousePosition().Y - this.YPosition;
		Position = new Vector2(x, y);
	}


	private void _on_area_2d_collision_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
		if (Input.IsActionJustPressed("on_right_click")){
			isSelected = true;
			this.Modulate = new Color("ffffffbb");
			this.ZIndex = 5;
			GD.Print("mouse position " + GetGlobalMousePosition().X  + GetGlobalMousePosition().Y);
			var x = GetGlobalMousePosition().X - this.XPosition;
			var y = GetGlobalMousePosition().Y - this.YPosition;
			GD.Print("object position " + x + " " + y);

		} else if(Input.IsActionJustPressed("on_left_click") && isSelected) {
			isSelected = false;
			this.Modulate = new Color("ffffff");
			this.ZIndex = 0;
		}
	}
}
