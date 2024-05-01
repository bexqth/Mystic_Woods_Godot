using Godot;
using System;

public partial class DayNightCycle : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private bool day;
	private bool night;
	private float dayTime = 30;
	private float nightTime = 15;
	private string state = "";
	private string stateDay = "day";
	private string stateNight = "night";
	private ColorRect colorRect;
	private AnimationPlayer animationPlayer;
	private Timer timer;
	private bool changingState;
	public override void _Ready()
	{
		this.day = true;
		this.night = false;
		this.colorRect = GetNode<ColorRect>("ColorRect");
		this.animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		this.timer = GetNode<Timer>("Timer");
		this.state = this.stateDay;
		this.colorRect.MouseFilter = Control.MouseFilterEnum.Ignore;
		Color currentColor = colorRect.Color;
		currentColor.A = 0;
		colorRect.Color = currentColor;
		this.timer.WaitTime = dayTime;
		this.timer.Start();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(this.changingState) {
			this.changingState = false;
			this.changeState();
		}
	}

	public void changeState() {
		if(this.state == stateDay) {
			//this.changeToDay();
			this.changeTo("night_day_fade", dayTime);
		} else if (this.state == stateNight) {
			//this.changeToNight();
			this.changeTo("day_night_fade", nightTime);
		}
	}

	public void changeTo(string animation, float waitTime) {
		animationPlayer.Play(animation);
		timer.WaitTime = waitTime;
		timer.Start();
	}

	private void _on_timer_timeout()
	{
		if(this.state == this.stateDay) {
			this.state = this.stateNight;
		} else if (this.state == this.stateNight) {
			this.state = this.stateDay;
		}
		this.changingState = true;
	}
}





