using Godot;
using System;

public partial class CraftingUI : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Slot[] slots;
	private Slot[] craftingSlots;
	[Export]
	public Slot slot1;
	[Export]
	public Slot slot2;
	[Export]
	public Slot slot3;
	[Export]
	public Slot slot4;
	[Export]
	public Slot slot5;
	[Export]
	public Slot slot6;
	[Export]
	public Slot slot7;
	[Export]
	public Slot slot8;
	[Export]
	public Slot slot9;
	[Export]
	public Slot slot10;
	private Slot clickedSlot;
	private World world;

	public override void _Ready()
	{
		world = (World)GetNode("/root/World");

		slots = new Slot[] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10};
		//craftingSlots = new Slot[] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9};
		//setSlotsIntoInventory();
		foreach (var slot in slots) {
			slot.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
			slot.FocusMode = Control.FocusModeEnum.None;
		}
		//slot10.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public Slot[] getSlots() {
		return this.slots;
	}

	public void onSlotClicked(Slot slot){
		clickedSlot = slot;
		if(world.getPlayerUsingTable()) {
			if(world.getDraggedItem() == null) { //PICK UP
				clickedSlot.pickUpItem();
				world.setDraggedItem(clickedSlot.getSelectedItem());
			} else {                             //STORE
				clickedSlot.storeItem(world.getDraggedItem());
			}
		}
	}

	public void setResultItem(PackedScene resultItemScene) {
		if(resultItemScene != null) {
			removeStack();
			InventoryItem resultItem = (InventoryItem)resultItemScene.Instantiate();
			this.slot10.addItemToArray(resultItem);
			this.slot10.setIcon(resultItem.getIcon());
		}
		
	}

	public void removeStack() {
		for(int i = 0; i < this.slots.Length - 1; i++) {
			if(slots[i].getItem() != null) {
				slots[i].deleteItemAtAll();
			}
		}
	}		
	
}
