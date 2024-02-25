using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	private const float speed = 50;
	private bool chasePlayer;
	private Player player;
	private Area2D detectionArea;
	private AnimatedSprite2D animator;
	private int damage = 10;
	private int health = 100;
	private String enemyType; 
	private bool attackCooldownTurnOn;
	private Timer attackCooldownTimer;
	private ProgressBar enemyHealthbar;

	public override void _Ready() {
		base._Ready();
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		attackCooldownTimer = GetNode<Timer>("take_attack_cooldown");
		enemyHealthbar = GetNode<ProgressBar>("enemy_healthbar");
	}

	public override void _PhysicsProcess(double delta) {
		movement();
		checkForBeingHit();
		checkHealth();
		manageHealthBar();
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


	public void _on_area_2d_body_entered(Player body) {
		player = body;
		chasePlayer = true;
		enemyType = body.Name;
		//GD.Print(enemyType);
	}

	public void dealDamage(int damage) {
		if (!attackCooldownTurnOn) {
			health -= damage;
			attackCooldownTurnOn = true;
			attackCooldownTimer.Start();
			//GD.Print(health);
		}
	}

	private void _on_take_attack_cooldown_timeout(){
		attackCooldownTurnOn = false;
	}

	public void _on_area_2d_body_exited(Node2D body) {
		player = null;
		chasePlayer = false;
 	}

	public int getAttackDamage() {
		return damage;
	}

	public void checkForBeingHit() {
		switch(enemyType){
			case "Player":
				if(player.getAttacking()) {
					dealDamage(player.getDamage());
					//GD.Print("Enemy health is " + this.health);
					checkHealth();
				}	
			break;
		}		
	}

	public void manageHealthBar() {
		enemyHealthbar.Value = health;
		//GD.Print(enemyHealthbar.Value);

		if(health >= 100) {
			enemyHealthbar.Modulate = new Color("#75caa7");
		} else if (health <= 70 && health >= 30) {
			enemyHealthbar.Modulate = new Color("#e1aa6a");
		} else {
			enemyHealthbar.Modulate = new Color("#fe9296");
		}
	}

	public void checkHealth() {
		if(this.health <= 0) {
			this.die();
		}
	}

	public void die() {
		animator.Play("die");
		 Free();
	}
}

