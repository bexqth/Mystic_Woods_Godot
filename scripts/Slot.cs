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
	public override void _Ready()
	{
		icon = GetNode<Texture2D>("slotItemIcon");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void setIcon(Texture2D newIcon)
    {
        icon = newIcon;
    }

    public bool getIsFree()
    {
        return this.isFree;
    }
}
