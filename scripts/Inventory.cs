using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Inventory : Node
{
	// Called when the node enters the scene tree for the first time.
	//InventoryItem[] items;
	private int inventorySize = 5;
	
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
	public override void _Ready()
	{
		slots = new Slot[] { slot1, slot2, slot3, slot4, slot5 };
		setSlotsIntoInventory();
		focusIndex = 0;	
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

	public void addItem(InventoryItem item) {
		for(int i = 0; i < inventorySize; i++) {
			if(slots[i].getSlotItemName() == item.getItemName()) {
				InventoryItem itemCopy = (InventoryItem)item.Duplicate();
				slots[i].setIcon(itemCopy.getIcon()); 
				slots[i].addItemToArray(itemCopy); 

				//GD.Print("object name " + itemCopy.getItemName());
				//GD.Print("slot name " + slots[i].getSlotItemName());

				item.QueueFree(); 
				return;
			}
		}

		for(int i = 0; i < inventorySize; i++) {
			if(slots[i].getIsFree() == true) {
				InventoryItem itemCopy = (InventoryItem)item.Duplicate();
				slots[i].setIcon(itemCopy.getIcon());
				slots[i].addItemToArray(itemCopy);
				slots[i].setSlotItemName(item.getItemName());

				//GD.Print("object name " + itemCopy.getItemName());
				//GD.Print("slot name " + slots[i].getSlotItemName());

				slots[i].setFree(false);
				item.QueueFree();
				return;
			}
		}
	}

	public void deleteItem() {
		slots[focusIndex].deleteItemFromArray();
	}

	public void useInventoryItem(InventoryItem item) {

	}

	public void changeFocus() {
		if (Input.IsActionJustPressed("scroll_up")) {
			//GD.Print("Mouse wheel scrolled up!");
			focusIndex++;
			if (focusIndex >= inventorySize)
				focusIndex = 0;
		}
		else if (Input.IsActionJustPressed("scroll_down")){
			//GD.Print("Mouse wheel scrolled down!");
			focusIndex--;
			if (focusIndex < 0)
				focusIndex = inventorySize - 1;
		}
		slots[focusIndex].GrabFocus();
		//slots[focusIndex].GetNode<Button>("deleteButton").GrabFocus();
	}

}
