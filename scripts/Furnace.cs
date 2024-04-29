using Godot;
using System;

public partial class Furnace : Node2D //MORE LIKE MANAGER FOR THE FURNICE
{

	private PackedScene furnaceUIScene;
	private FurnaceUI furnaceUI;
	private PackedScene resultItemScene;
	private InventoryItem resultItem;
	private AnimatedSprite2D animator;
	private bool canUseFurnace;
	private bool isBeingUsed;
	private World world;
	private bool furnaceUIVisible;
	private Label text;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		furnaceUIScene = GD.Load<PackedScene>("res://scenes/furnace_ui.tscn");
		furnaceUI = (FurnaceUI)furnaceUIScene.Instantiate();
		AddChild(furnaceUI);
		this.animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		world = (World)GetNode("/root/World");
		furnaceUIVisible = false;
		furnaceUI.Visible = false;
		text = GetNode<Label>("Label");
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		handleFurnace();
		createItem();
		base._Process(delta);
	}

	public void createItem() {
		if(furnaceUI.getMaterialItem() != null && furnaceUI.getSourceLightItem() != null) {
			if(this.furnaceUI.getSourceLightItem().getItemName() == "Coal") {
				
				ResourceItem materialItem = (ResourceItem)furnaceUI.getMaterialItem();
				switch(furnaceUI.getMaterialItem().getItemName()) {
					case "IronRock":
						resultItemScene = GD.Load<PackedScene>("res://scenes/iron.tscn");
					break;

					default:
						resultItemScene = null;
					break;
				}
				if(resultItemScene != null) {
					animator.Play("cooking");
					furnaceUI.playCookingAnimation(materialItem.getCookingTime());

					if(!furnaceUI.getCooking()) {
						//GD.Print("Done cooking");
						furnaceUI.removeStack();
						furnaceUI.setResultItem(resultItemScene);
						//furnaceUI.getResultSlot().setIcon(icon);
						animator.Play("fire_dying");
					}
				}
				

			} else {
				//GD.Print("U need to use coal as the source light");
			}
		}
	}

	private void _on_area_2d_range_body_entered(Node2D body)
	{
		if(body is Player player) {
			text.Text = "Press E to use furnace";
			animator.Play("using");
			canUseFurnace = true;
			GD.Print("Player in the range");
			//canBeHit = true;
			//this.player = player;
		}
	}


	private void _on_area_2d_range_body_exited(Node2D body)
	{
		if(body.Name == "Player") {
			clearText();
			animator.Play("unUsing");
			canUseFurnace = false;
			if(furnaceUIVisible) {
				furnaceUI.Visible = false;
				furnaceUIVisible = false;
			}
			//world.setPlayerNearChest(false);
		}
	}

	public void handleFurnace() {
		if(Input.IsActionJustPressed("pressed_e") && !isBeingUsed && canUseFurnace) {
			furnaceUI.Visible = true;
			furnaceUIVisible = true;
			isBeingUsed = true;
			world.setPlayerUsingFurnace(true);
		} else if(Input.IsActionJustPressed("pressed_e") && isBeingUsed) {
			furnaceUI.Visible = false;
			furnaceUIVisible = false;
			isBeingUsed = false;
			world.setPlayerUsingFurnace(false);
		}
	}

	public void clearText() {
		text.Text = "";
	}

}
