using Godot;
using Godot.NativeInterop;
using System;
using System.Runtime.CompilerServices;

public partial class Inventory : Node
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
	private InventoryItem draggerItem;
	public override void _Ready()
	{
		slots = new Slot[] { slot1, slot2, slot3, slot4, slot5 };
		setSlotsIntoInventory();
		focusIndex = 0;	
		foreach (var slot in slots) {
			slot.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
		}
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		changeFocus();
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
		if(draggerItem == null) {
			clickedSlot.pickUpItem();
			draggerItem = clickedSlot.getSelectedItem();
			draggerItem = null;
		} else {
			clickedSlot.storeItem(draggerItem);
			draggerItem = null;
		}
	}

	/*public void OnItemPickedUp(InventoryItem item) {
		selectedItem = item;
	}*/

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
		slots[focusIndex].deleteItemFromArray();
	}

	public void changeFocus() {
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
