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

	private Button deleteButton;
	private InventoryItem selectedItem;
	private bool isHoldingItem;
	private bool canStoreItem;
	private bool isClickedOn;
	private bool isTryingToPickUpItem;
	private bool isTryingToStoreItem;
	private InventoryItem draggedItem;
	public override void _Ready()
	{
		items = new List<InventoryItem>();
		textureRect = GetNode<TextureRect>("slotItemIcon");
		texture2d = textureRect.Texture;
		countLabel = GetNode<Label>("countLabel");
		deleteButton = GetNode<Button>("deleteButton");
		isFree = true;
		isHoldingItem = false; 
		canStoreItem = false;
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
	}

	public InventoryItem getItem() {
		return items[0];
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
		World world = (World)GetNode("/root/World");
		//GD.Print(world.getPlayerNearChest());
		if(world.getPlayerNearChest()) {
			//GD.Print(world.getPlayerNearChest());
			if (items.Count > 0) {
			selectedItem = items[0];
			items.Remove(selectedItem);
			draggedItem = selectedItem;

			GetTree().CurrentScene.AddChild(selectedItem);
			selectedItem.setSelected(true);
			selectedItem.setInInventory(false);

			world.setDraggedItem(selectedItem);

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
		/*InventoryItem itemCopy = (InventoryItem)item.Duplicate();
		setIcon(itemCopy.getIcon()); 
		addItemToArray(itemCopy); 
		item.QueueFree(); */

		/*selectedItem = item;
		addItemToArray(selectedItem); 

		setIcon(selectedItem.getIcon()); 
		
		selectedItem.setInInventory(true);
		setSlotItemName(selectedItem.getItemName());
		selectedItem.setSelected(false);
		selectedItem = null;

		//GetTree().CurrentScene.RemoveChild(item);
		//item.QueueFree();*/

		InventoryItem itemCopy = (InventoryItem)item.Duplicate();
		setIcon(itemCopy.getIcon());
		addItemToArray(itemCopy);
		itemCopy.setInInventory(true);
		setSlotItemName(item.getItemName());
		setFree(false);
		item.QueueFree();
	}
	
	private void _on_mouse_entered()
	{
		GrabFocus();
	}

	public InventoryItem getDraggedItem() {
		return draggedItem;
	}
}

