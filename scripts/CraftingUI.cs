using Godot;
using System;

public partial class CraftingUI : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Slot[] slots;
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
		setSlotsIntoInventory();
		foreach (var slot in slots) {
			slot.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
			slot.FocusMode = Control.FocusModeEnum.None;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void setSlotsIntoInventory() {
		slots[0] = slot1;
		slots[1] = slot2;
		slots[2] = slot3;
		slots[3] = slot4;
		slots[4] = slot5;
		slots[5] = slot6;
		slots[6] = slot7;
		slots[7] = slot8;
		slots[8] = slot9;
		slots[9] = slot10;
	}

	public void onSlotClicked(Slot slot){
		clickedSlot = slot;
		if(world.getPlayerOpenedChest()) {
			if(world.getDraggedItem() == null) { //PICK UP
				clickedSlot.pickUpItem();
				world.setDraggedItem(clickedSlot.getSelectedItem());
			} else {                             //STORE
				clickedSlot.storeItem(world.getDraggedItem());
			}
		}
	}


	
}
