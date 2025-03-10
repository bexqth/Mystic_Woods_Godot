 using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Slot : Button
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public String slotItemName;
	[Export] 
	public Texture2D icon;
	[Export] 
	public bool isFree;
	[Export] 
	public int index;

	[Signal]
	public delegate void SlotClikedEventHandler(Slot slot); //creating the signal

	private TextureRect textureRect;
	private Texture2D texture2d;
	private Label countLabel;
	private List<InventoryItem> items;

	private InventoryItem selectedItem;
	private bool isHoldingItem;
	private bool canStoreItem;
	private bool isClickedOn;
	private bool isTryingToPickUpItem;
	private bool isTryingToStoreItem;
	private InventoryItem draggedItem;
	private World world;
	private bool storedItem;
	public override void _Ready()
	{
		items = new List<InventoryItem>();
		textureRect = GetNode<TextureRect>("slotItemIcon");
		texture2d = textureRect.Texture;
		countLabel = GetNode<Label>("countLabel");
		isFree = true;
		isHoldingItem = false; 
		canStoreItem = false;
		world = (World)GetNode("/root/World");
		storedItem = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(Disabled); 
	}

	public void addItemToArray(InventoryItem newItem) {
		items.Add(newItem);
		if(newItem.getStackable()) {
			countLabel.Text = items.Count.ToString();
		}
	}

	public void setIcon(Texture2D newIcon)
	{	
		textureRect.Texture = newIcon;
	}

	public void setSlotItemName(String s) {
		this.slotItemName = s;
	}

	public String getSlotItemName() {
		return this.slotItemName;
	}

	public bool getIsFree()
	{
		return this.isFree;
	}

	public void setFree(bool v)
	{
		this.isFree = v;
	}

	public void deleteItemFromArray() {
		InventoryItem item = items[0];
		//GD.Print("deleted item " + item.printCollision());
		item.setPositionAfterDeletingFromItem();
		GetTree().CurrentScene.AddChild(item);

		items.RemoveAt(0);
		if(items.Count > 0) {
			textureRect.Texture = items[0].getIcon(); 
			countLabel.Text = items.Count.ToString();
		} else {
			textureRect.Texture = null;
			countLabel.Text = " ";
			slotItemName = " ";
			isFree = true;
		}
		//GD.Print("delete done");
	}

	public void deleteItemAtAll() {
		InventoryItem item = items[0];
		//GD.Print("deleted item " + item.printCollision());

		items.RemoveAt(0);
		if(items.Count > 0) {
			textureRect.Texture = items[0].getIcon(); 
			countLabel.Text = items.Count.ToString();
		} else {
			textureRect.Texture = null;
			countLabel.Text = " ";
			slotItemName = " ";
			isFree = true;
		}
		item.QueueFree();
		//GD.Print("delete done");
	}

	public InventoryItem getItem() {
		if(items.Count > 0) {
			return items[0];
		}
		return null;
	}

	private void _on_delete_button_pressed() {

	}

	private void _on_pressed()
	{
		EmitSignal(nameof(Slot.SlotCliked), this);
		// this signals the signal, so when its cliked it emits the signal 
		//and the inventorty need to catch it now	
	}

	public bool getIsClikedOn() {
		return isClickedOn;
	}

	public bool getIsTryingToPickUp() {
		return isTryingToPickUpItem;
	}

	public bool getIsTryingToStoreItem() {
		return isTryingToStoreItem;
	}

	public InventoryItem getSelectedItem() {
		return selectedItem;
	}

	public void setSelectedItemNull() {
		selectedItem = null;
	}
 
	public void pickUpItem() {
		//GD.Print(world.getPlayerNearChest());
		if(world.getPlayerOpenedChest() || world.getPlayerUsingTable() || world.getPlayerUsingFurnace()) {
			//GD.Print(world.getPlayerNearChest());
			if (items.Count > 0) {
				selectedItem = items[0];
				selectedItem.turnOffColision();
				selectedItem.ZIndex = 5;
				items.Remove(selectedItem);
				setSlotItemName(selectedItem.getItemName());
				GetTree().CurrentScene.AddChild(selectedItem);
				selectedItem.setSelected(true);
				selectedItem.setInInventory(false);
				selectedItem.followMouse();

				if(items.Count > 0) {
					textureRect.Texture = items[0].getIcon(); 
					countLabel.Text = items.Count.ToString();
				} else {
					textureRect.Texture = null;
					countLabel.Text = " ";
					slotItemName = " ";
					isFree = true;
				}
			}	
		} else {
			GD.Print("u cant pick up an item");
		}
	}

	public void storeItem(InventoryItem item) {
		if(world.getPlayerOpenedChest() || world.getPlayerUsingTable() || world.getPlayerUsingFurnace()) {
			if(getSlotItemName() == item.getItemName() || items.Count == 0) {
				InventoryItem itemCopy = (InventoryItem)item.Duplicate();
				setIcon(itemCopy.getIcon());
				addItemToArray(itemCopy);
				setSlotItemName(itemCopy.getItemName());
				world.setDraggedItem(null);
				itemCopy.setInInventory(true);
				itemCopy.turnOnCollision();
				itemCopy.ZIndex = 1;
				setFree(false);
				GetTree().CurrentScene.RemoveChild(item);
				item.QueueFree();		
			} else {
				GD.Print("item is not the same as the one there");
			}
		}
	}

	
	private void _on_mouse_entered()
	{
		GrabFocus();
	}

	public InventoryItem getDraggedItem() {
		return draggedItem;
	}
}

