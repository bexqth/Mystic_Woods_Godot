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
	public override void _Ready()
	{
		textureRect = GetNode<TextureRect>("slotItemIcon");
		texture2d = textureRect.Texture;
		countLabel = GetNode<Label>("countLabel");
		deleteButton = GetNode<Button>("deleteButton");
		isFree = true;
		items = new List<InventoryItem>();
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

	public void pickUpItem() {
		if (items.Count > 0) {
			selectedItem = items[0];
			GetTree().CurrentScene.AddChild(selectedItem);
			selectedItem.setSelected(true);
			selectedItem.setInInventory(false);


			selectedItem.followMouse();
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
		
	}

	public void storeItem(InventoryItem item) {
		canStoreItem = false;
		GD.Print("Trying to store it");
		addItemToArray(item);
		setIcon(item.getIcon());
		setSlotItemName(item.getItemName());
		item.setSelected(false);
		item.setInInventory(true);
		GetTree().CurrentScene.RemoveChild(item);
		selectedItem = null;
	}
}
