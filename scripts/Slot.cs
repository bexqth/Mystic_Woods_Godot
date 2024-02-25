using Godot;
using System;

public partial class Slot : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public String name;
	[Export] 
	public Texture2D icon;
	[Export] 
	public bool isFree;
	[Export] 
	public int index;

	private TextureRect textureRect;
	private Texture2D texture2d;
	public override void _Ready()
	{
		textureRect = GetNode<TextureRect>("slotItemIcon");
    	texture2d = textureRect.Texture;
		isFree = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void setIcon(Texture2D newIcon)
    {	
        textureRect.Texture = newIcon;
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
