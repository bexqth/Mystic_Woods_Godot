using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Slot : Node
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
	public override void _Ready()
	{
		textureRect = GetNode<TextureRect>("slotItemIcon");
    	texture2d = textureRect.Texture;
		countLabel = GetNode<Label>("countLabel");
		isFree = true;
		items = new List<InventoryItem>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void addItemToArray(InventoryItem newItem) {
		items.Add(newItem);
		countLabel.Text = items.Count.ToString();
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

}
