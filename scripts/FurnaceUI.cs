using Godot;
using System;
using System.Collections.Generic;

public partial class FurnaceUI : Node2D
{

	private Slot[] slots;
	[Export]
	public Slot materialItem;
	[Export]
	public Slot lightSource; //coal
	[Export]
	public Slot resultItem;
	private AnimatedSprite2D animator;
	private bool doneCooking;
	private bool cooking;
	private InventoryItem draggedItem;
	private Slot clickedSlot;
	private World world;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		slots = new Slot[]{materialItem, lightSource, resultItem};
		this.animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		world = (World)GetNode("/root/World");
		animator.Play("default");
		this.doneCooking = false;
		this.cooking = false;
		foreach (var slot in slots) {
			slot.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
			slot.FocusMode = Control.FocusModeEnum.None;
		}	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void onSlotClicked(Slot slot){
		clickedSlot = slot;
		if(world.getPlayerUsingFurnace()) {
			if(world.getDraggedItem() == null) { //PICK UP
				clickedSlot.pickUpItem();
				world.setDraggedItem(clickedSlot.getSelectedItem());
			} else {                             //STORE
				clickedSlot.storeItem(world.getDraggedItem());
			}
		}
	}

	public InventoryItem getMaterialItem() {
		return this.materialItem.getItem();
	}

	public void setResultItem(PackedScene resultItemScene) {
		InventoryItem resultItem = (InventoryItem)resultItemScene.Instantiate();
		this.resultItem.addItemToArray(resultItem);
		this.resultItem.setIcon(resultItem.getIcon());
	}

	public InventoryItem getSourceLightItem() {
		return this.lightSource.getItem();
	}

	public Slot getResultSlot() {
		return this.resultItem;
	}

	public void removeStack() {
		this.materialItem.deleteItemAtAll();
		this.lightSource.deleteItemAtAll();

	}

	public void playCookingAnimation(int time) {
		this.animator.SpeedScale = 1f/time;
		this.cooking = true;
		animator.Play("cooking");
		if(animator.Frame == 10) {
			animator.Play("default");
			this.cooking = false;
			this.animator.SpeedScale = 1;
		}
	}

	public bool getCooking() {
		return this.cooking;
	}

}
