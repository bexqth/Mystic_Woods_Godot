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

	private TextureRect textureRect;
	private Texture2D texture2d;
	private Label countLabel;
	private List<InventoryItem> items;

	private Button deleteButton;
	public override void _Ready()
	{
		textureRect = GetNode<TextureRect>("slotItemIcon");
		texture2d = textureRect.Texture;
		countLabel = GetNode<Label>("countLabel");
		deleteButton = GetNode<Button>("deleteButton");
		isFree = true;
		items = new List<InventoryItem>();
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
		/*GD.Print("Delete button pressed!");
		//GD.Print("BUTTON CLICKED 1");
		InventoryItem item = items[0];
		item.setPositionAfterDeletingFromItem();
		GetTree().CurrentScene.AddChild(item);
		deleteItemFromArray();
		//GD.Print("ITEM DELETED");*/
	}


}
