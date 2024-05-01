using Godot;
using System;

public partial class HoeTool : InventoryItem
{
	private TileMap tileMap;
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
		var world = World.Instance;
		this.tileMap = world.GetTileMap();

		if (player.getCanHoe() && !player.getHoeing()) {
			player.setHoeing(true);
			player.setCanHoe(false);
			player.getHoeCoolDownTimer().Start();
			/*int sourceIdGrass = 2;
			Vector2I grassTile = new Vector2I(1,1);
			Vector2I tileMousePosition = tileMap.LocalToMap(player.globalMousePosition());

			if(Math.Abs((int)player.GlobalPosition.X/48 - tileMousePosition.X) < 2 && Math.Abs((int)player.GlobalPosition.Y/48 - tileMousePosition.Y) < 2) {
				tileMap.SetCell(0, tileMousePosition, sourceIdGrass, grassTile);
			} else{
				GD.Print("out of reach");
			}*/
				
		}
	}
}

