using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

public partial class InventoryItem : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public String itemName;
	[Export] 
	public Texture2D icon;
	[Export] 
	public bool stackable;
	[Export] 
	public int index;
	[Export] 
	public bool isInInventory;
	[Export]
	public Player player;
	private int count;
	private bool isSelected;
	private bool onFloor;
	private World world;
	//private World world;
	public override void _Ready()
	{
		this.icon = GetNode<Sprite2D>("Sprite2D").Texture;
		this.world = (World)GetNode("/root/World");
		this.player = GetTree().CurrentScene.GetNode<Player>("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(isSelected) {
			followMouse();
		}
	}

	public World GetWorld() {
		return this.world;
	}

	public void setIsInInventory(bool b) {
		isInInventory = b;
	}

	public bool getIsInInventory() {
		return isInInventory;
	}

	public Texture2D getIcon()
	{
		return this.icon;
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		/*if(body.Name == "Player") {
			player.addItemToInventory(this);
		}*/
	}

	public String getItemName() {
		if(itemName == null) {
			return "";
		}
		return this.itemName;
	}

	public virtual void useItem() {
		
	}

	public void turnOffColision() {
		GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Disabled = true;
		//GD.Print("Colision of the holding item is turned off");
	}

	public void turnOnCollision() {
		GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Disabled = false;
		//GD.Print("Colision of the holding item is turned on");
	}

	public String printCollision() {
		if(GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Disabled){
			return "is disabled";
		}
		return "is not disabled";
	}

	public void setPositionAfterDeletingFromItem() {
		Vector2 position = Position;
		//position.X = world.getPlayerPosition().X;
		//position.Y = world.getPlayerPosition().Y - 50;
		position.X = player.Position.X;
		position.Y = player.Position.Y - 50;
		Position = position;
	}

	public bool getStackable()
	{
		return stackable;
	}
	
	private void _on_area_2d_input_event(Node viewport, InputEvent @event, long shape_idx) {
		if (Input.IsActionJustPressed("on_left_click") && isSelected){
			isSelected = false;
		}
	}

	public void setInInventory(bool b) {
		isInInventory = b;
	}

	public void followMouse() {
		Position = GetGlobalMousePosition();
	}

	public void setSelected(bool b) {
		isSelected = b;
	}
}

