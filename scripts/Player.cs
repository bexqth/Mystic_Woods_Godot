using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private  float speed = 5.0f;
	private bool isIdle;
	private bool isRunning;
	private bool isAttacking;
	private bool isDead;
	private int direction;
	private int health;
	private bool canGetHit;
	private bool attackCooldownTurnOn;
	private Timer attackCooldownTimer;
	private string enemyType;
	
	private AnimatedSprite2D animator;
	public override void _Ready()
	{
		base._Ready();
		setRunning(false);
		health = 100;
		canGetHit = false;
		attackCooldownTurnOn = false;
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		attackCooldownTimer = GetNode<Timer>("take_attack_cooldown");
	}

	public override void _PhysicsProcess(double delta)
	{
		keyboardControl();
		setAnimation();
		checkForBeingHit();
		checkHealth();
	}

	public void keyboardControl() {
		setRunning(false);
		Vector2 position = Position;
		Vector2 scale = animator.Scale;

		if(Input.IsKeyPressed(Key.A)) {
			position.X -= speed;
			scale.X = -1;
			setRunning(true);	
		} else if(Input.IsKeyPressed(Key.D)) {
			position.X += speed;
			scale.X = 1;
			setRunning(true);
		} else if (Input.IsKeyPressed(Key.W)) {
			
			position.Y -= speed;
			setRunning(true);
		} else if(Input.IsKeyPressed(Key.S)) {
			
			position.Y += speed;	
			setRunning(true);
		}

		Position = position;
		animator.Scale = scale;
		MoveAndSlide();
	}
	

	public void setRunning(bool value) {
		isRunning = value;
		isIdle = !value;
	}

	public void setAnimation(){
		if(isRunning) {
			animator.Play("run");
		} else if (isIdle) {
			animator.Play("idle");
		}
	}

	private void _on_player_hitbox_body_entered(CharacterBody2D body) {
		if(body.Name == "SlimeEnemy") {
			canGetHit = true;
			enemyType = body.Name;
		}

	}

	private void _on_player_hitbox_body_exited(Node2D body) {
		if(body.Name == "SlimeEnemy") {
			canGetHit = false;
		}

	}


	public void dealDamage(int damage) {
		if (!attackCooldownTurnOn) {
			health -= damage;
			attackCooldownTurnOn = true;
			attackCooldownTimer.Start();
			GD.Print(health);
		}

	}

	public void checkForBeingHit() {
		if (canGetHit) {
			switch(enemyType){
				case "SlimeEnemy":
					dealDamage(new Enemy().getAttackDamage());
				break;
			}		
		}
	}
	private void _on_take_attack_cooldown_timeout() {
		attackCooldownTurnOn = false;
	}
	public void checkHealth() {
		if(health <= 0) {
			die();
		}
	}


	public void die() {
		canGetHit = false;
		animator.Play("die");
	}
}






