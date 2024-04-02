using Godot;
using System;

public partial class Recipe : Node2D
{
	private String[] recipeArray;
	private String resultItem;
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void setRecipe(String[] items) {
		recipeArray = new String[9];
		this.recipeArray = items;
	}

	public String[] getRecipeArray() {
		return this.recipeArray;
	}

	public String getResultItemName() {
		return this.resultItem;
	}

	public void setResultItemName(String s) {
		this.resultItem = s;
	}

}
