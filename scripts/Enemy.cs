using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	private const float speed = 50;
	private bool chasePlayer;
	private Node2D player;
	private Area2D detectionArea;
	private AnimatedSprite2D animator;
	private int damage = 20;
	private int health = 100;

	public override void _Ready()
	{
		base._Ready();
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta) {
		movement();
	}

	public void movement() {
		Vector2 scale = animator.Scale;
		Vector2 position = Position;
		if(chasePlayer) {
			position += (player.Position - position)/speed;
			animator.Play("move");
			if(Position.X - player.Position.X > 0) {
				scale.X = -1;	
			} else {
				scale.X = 1;
			}
			Position = position;
			animator.Scale = scale;
		} else {
			animator.Play("idle");
		}
	}


	public void _on_area_2d_body_entered(Node2D body) {
		player = body;
		chasePlayer = true;
	}

	public void dealDamage(int damage) {
		this.health -= damage;
	}

	public void _on_area_2d_body_exited(Node2D body) {
		player = null;
		chasePlayer = false;
 	}

	public void attackPlayer() {

	}

	public int getAttackDamage() {
		return damage;
	}
}

