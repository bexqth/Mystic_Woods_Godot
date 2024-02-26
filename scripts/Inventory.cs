using Godot;
using System;

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
	public override void _Ready()
	{
		slots = new Slot[] { slot1, slot2, slot3, slot4, slot5 };
		setSlotsIntoInventory();
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
	}

	/*public void addItem(InventoryItem item) {
		
		for(int i = 0; i < inventorySize; i++) {
			if(slots[i].getSlotItemName() == item.getItemName()) {
				slots[i].addItemToArray(item);
				//item.QueueFree();
				item.GetParent().RemoveChild(item);
			} else if(slots[i].getIsFree() == true) {
				slots[i].setIcon(item.getIcon());
				slots[i].addItemToArray(item);
				slots[i].setSlotItemName(item.getItemName());
				slots[i].setFree(false);
				//item.QueueFree();
				item.GetParent().RemoveChild(item);
				break;
			}
		}
	}*/
	public void addItem(InventoryItem item) {
		GD.Print(item.icon);
	for(int i = 0; i < inventorySize; i++) {
		if(slots[i].getSlotItemName() == item.getItemName()) {
			InventoryItem itemCopy = (InventoryItem)item.Duplicate();
			slots[i].setIcon(itemCopy.getIcon());
			slots[i].addItemToArray(itemCopy); 
			item.QueueFree(); 
		} else if(slots[i].getIsFree() == true) {
			InventoryItem itemCopy = (InventoryItem)item.Duplicate();
			slots[i].setIcon(itemCopy.getIcon());
			slots[i].addItemToArray(itemCopy);
			slots[i].setSlotItemName(item.getItemName());
			slots[i].setFree(false);
			item.QueueFree();
			break;
		}
	}
}


	public void useInventoryItem(InventoryItem item) {

	}
}
