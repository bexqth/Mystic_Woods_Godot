using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CraftingTable : PlacableItem
{
	private PackedScene craftingUIScene;
	private CraftingUI craftingUI;
	private AnimatedSprite2D animator;
	private World world;
	private bool canUseTable;
	private bool isBeingUsed;
	private bool craftingUIVisible;
	private Label text;
	private String resultRecipe;
	private String resultItem;

	private PackedScene resultItemPackedScene;
	private List<Recipe> recipes;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animator.Play("notUsing");
		craftingUIScene = GD.Load<PackedScene>("res://scenes/crafting_ui.tscn");
		craftingUI = (CraftingUI)craftingUIScene.Instantiate();
		AddChild(craftingUI);
		text = GetNode<Label>("Label");
		world = (World)GetNode("/root/World");
		craftingUIVisible = false;
		craftingUI.Visible = false;
		recipes = new List<Recipe>();
		createRecipes();
		base._Ready();
	}


	public override void _Process(double delta)
	{
		handleTable();
		createItem();
		base._Process(delta);
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body.Name == "Player") {
			text.Text = "Press E to use crafting table";
			animator.Play("using");
			canUseTable = true;
			//world.setPlayerNearChest(true);
		}
	}


	private void _on_area_2d_body_exited(Node2D body)
	{
		if(body.Name == "Player") {
			clearText();
			animator.Play("unUsing");
			canUseTable = false;
			if(craftingUIVisible) {
				craftingUI.Visible = false;
				craftingUIVisible = false;
			}
			//world.setPlayerNearChest(false);
		}
	}

	public void createItem() {
		if(this.world.getPlayerUsingTable()) {
			compareSlots();
			switch(resultItem) {
				case "ironPickaxe":
					this.resultItemPackedScene = GD.Load<PackedScene>("res://scenes/stone_pickaxe.tscn");
				break;

				case "ironAxe":
					this.resultItemPackedScene = GD.Load<PackedScene>("res://scenes/stone_axe.tscn");
				break;
			}
			craftingUI.setResultItem(this.resultItemPackedScene);
			resultItemPackedScene = null;
		}
		
	}

	public void createRecipes() {
		Recipe ironPickaxe = new Recipe();
		ironPickaxe.setRecipe(new string[9]{"Stone", "Stone", "Stone", "", "Log", "", "", "Log", ""});
		ironPickaxe.setResultItemName("ironPickaxe");
		
		Recipe ironAxe = new Recipe();
		ironAxe.setRecipe(new string[9]{"", "Stone", "Stone", "", "Log", "Stone", "", "Log", ""});
		ironAxe.setResultItemName("ironAxe");

		this.recipes.Add(ironPickaxe);
		this.recipes.Add(ironAxe);
	}

	public void compareSlots() {
		for(int i = 0; i < this.recipes.Count; i++) {
			for(int j = 0; j < this.craftingUI.getSlots().Length - 1; j++) {
				if(this.craftingUI.getSlots()[j].getItem() == null && this.recipes[i].getRecipeArray()[j] != "") {
					break;
				} else if(this.craftingUI.getSlots()[j].getItem() != null && this.recipes[i].getRecipeArray()[j] != this.craftingUI.getSlots()[j].getItem().getItemName()) {
					break;
				}
				if(j == this.craftingUI.getSlots().Length - 2) {
					resultItem = recipes[i].getResultItemName();
					return;
				}
			}
		}
		resultItem = null;
	}

	public void handleTable() {
		if(Input.IsActionJustPressed("pressed_e") && !isBeingUsed && canUseTable) {
			craftingUI.Visible = true;
			craftingUIVisible = true;
			isBeingUsed = true;
			world.setPlayerUsingTable(true);
		} else if(Input.IsActionJustPressed("pressed_e") && isBeingUsed) {
			craftingUI.Visible = false;
			craftingUIVisible = false;
			isBeingUsed = false;
			world.setPlayerUsingTable(false);
		}
	}

	public void clearText() {
		text.Text = "";
	}

}
