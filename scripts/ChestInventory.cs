using Godot;
using System;

public partial class ChestInventory : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private int inventorySize = 15;
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
	[Export]
	public Slot slot11;
	[Export]
	public Slot slot12;
	[Export]
	public Slot slot13;
	[Export]
	public Slot slot14;
	[Export]
	public Slot slot15;
	private Slot clickedSlot;
	private Slot slotToDropItem;
	private InventoryItem draggedItem;
	[Signal]
	public delegate void pickedChestInventorySlotEventHandler(Slot slot); 

	public override void _Ready()
	{	
		slots = new Slot[] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot12, slot12, slot13, slot14, slot15 };
		setSlotsIntoInventory();
		foreach (var slot in slots) {
			GD.Print(slot);
			slot.Connect(nameof(Slot.SlotCliked), new Callable(this, nameof(onSlotClicked)));
			slot.FocusMode = Control.FocusModeEnum.None;
		}

		//TEST OBJECT
		PackedScene appleScene = GD.Load<PackedScene>("res://scenes/apple.tscn");
		InventoryItem apple = (InventoryItem)appleScene.Instantiate();
		slot1.addItemToArray(apple);
		slot1.setIcon(apple.getIcon());
		
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
		slots[10] = slot11;
		slots[11] = slot12;
		slots[12] = slot13;
		slots[13] = slot14;
		slots[14] = slot15;
	}


	public void onSlotClicked(Slot slot){
		clickedSlot = slot;
    	World world = (World)GetNode("/root/World");

    	if(world.getDraggedItem() == null) { //PICK UP
			clickedSlot.pickUpItem();
			world.setDraggedItem(clickedSlot.getSelectedItem());
			world.getDraggedItem().turnOffColision();
    	} else {                             //STORE
			clickedSlot.storeItem(world.getDraggedItem());
			world.getDraggedItem().setInInventory(true);
			world.getDraggedItem().turnOnCollision();
			world.setDraggedItem(null);
    	}
	}

}
