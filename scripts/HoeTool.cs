using Godot;
using System;

public partial class HoeTool : InventoryItem
{
	[Export]
	public TileMap tileMap;
	private PackedScene foodPlantScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void useItem() {
		if (player.getCanHoe() && !player.getHoeing()) {
			player.setHoeing(true);
			player.setCanHoe(false);
			player.getHoeCoolDownTimer().Start();
			int sourceIdGrass = 0;
			Vector2I grassTile = new Vector2I(1,1);	
			Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());
			tileMap.SetCell(0, tileMousePosition, sourceIdGrass, grassTile);	
		}
	}
}

