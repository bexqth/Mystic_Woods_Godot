using Godot;
using Godot.NativeInterop;
using System;
using System.Runtime.CompilerServices;

public partial class Inventory : Node2D
{
	// Called when the node enters the scene tree for the first time.
	//InventoryItem[] items;
	private int inventorySize = 5;
	private InventoryItem selectedItem;
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
	private int focusIndex;
	private Slot clickedSlot;
	private Slot slotToDropItem;
	private InventoryItem draggedItem;
	[Signal]
	public delegate void pickedInventorySlotEventHandler(Slot slot); 
	private World world;
	public override void _Ready()
	{
		slots = new Slot[] { slot1, slot2, slot3, slot4, slot5 };
		setSlotsIntoInventory();
		focusIndex = 0;	
		foreach (var slot in slots) {
			slot.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
			//slot.FocusMode = Control.FocusModeEnum.None;
		}
		world = (World)GetNode("/root/World");
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		changeFocus();
		changeFocusChestOpened();
	}

	public void setSlotsIntoInventory() {
		slots[0] = slot1;
		slots[1] = slot2;
		slots[2] = slot3;
		slots[3] = slot4;
		slots[4] = slot5;
	}

	public void onSlotClicked(Slot slot){
		clickedSlot = slot;
		if(world.getPlayerOpenedChest()) {
			if(world.getDraggedItem() == null) { //PICK UP
				clickedSlot.pickUpItem();
				world.setDraggedItem(clickedSlot.getSelectedItem());
				world.getDraggedItem().turnOffColision();
			} else {                             //STORE
				world.getDraggedItem().turnOnCollision();
				clickedSlot.storeItem(world.getDraggedItem());
				world.getDraggedItem().setInInventory(true);
				world.setDraggedItem(null);
			}
		}
    	
	}

	public void changeFocusChestOpened() {
		if(world.getPlayerOpenedChest()) {
			foreach (var slot in slots) {
				slot.FocusMode = Control.FocusModeEnum.None;
			}
		} else {
			foreach (var slot in slots) {
				slot.FocusMode = Control.FocusModeEnum.All;
			}
		}
		
	}

	public InventoryItem getHoldingItem() {
		if(!slots[focusIndex].isFree) {
			return slots[focusIndex].getItem();
		}
		return null;
	}

	public void addItem(InventoryItem item) {
		for(int i = 0; i < inventorySize; i++) {
			if(slots[i].getSlotItemName() == item.getItemName()) {
				InventoryItem itemCopy = (InventoryItem)item.Duplicate();
				slots[i].setIcon(itemCopy.getIcon()); 
				slots[i].addItemToArray(itemCopy); 
				itemCopy.setInInventory(true);
				item.QueueFree(); 
				return;
			}
		}

		if(!isFull()) {
			for(int i = 0; i < inventorySize; i++) {
				if(slots[i].getIsFree() == true) {
					InventoryItem itemCopy = (InventoryItem)item.Duplicate();
					slots[i].setIcon(itemCopy.getIcon());
					slots[i].addItemToArray(itemCopy);
					itemCopy.setInInventory(true);
					slots[i].setSlotItemName(item.getItemName());
					slots[i].setFree(false);
					item.QueueFree();
					return;
				}
			}
		}
	}

	public bool isFull()
	{
		int freeSpots =  0;
		for(int i = 0; i < inventorySize; i++) {
			if(slots[i].isFree) {
				freeSpots++;
			}
		}

		if(freeSpots > 0) {
			return false;
		}

		GD.Print("INVENTORY IS FULL");
		return true;
	}

	public void deleteItem() {
		GD.Print("index is" + focusIndex);
		slots[focusIndex].deleteItemFromArray();
	}


	public void changeFocus() {
		if(!world.getPlayerOpenedChest()) {
			if (Input.IsActionJustPressed("scroll_up")) {
				focusIndex++;
				if (focusIndex >= inventorySize)
					focusIndex = 0;
			}
			else if (Input.IsActionJustPressed("scroll_down")){
				focusIndex--;
				if (focusIndex < 0)
					focusIndex = inventorySize - 1;
			}
			slots[focusIndex].GrabFocus();					
		}		
	}

}
