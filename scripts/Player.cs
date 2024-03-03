using Godot;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Player : CharacterBody2D
{
	private  float speed = 5.0f;
	private bool isIdle;
	private bool isRunning;
	private bool isAttacking;
	private bool isDead;
	private int direction;
	private int health;
	private int hungerBar;
	private bool canGetHit;
	private bool attackCooldownTurnOn;
	private Timer attackCooldownTimer;
	private Timer giveAttackCoolDownTimer;
	private string enemyType;
	private bool canAttack;
	private int damage;
	private ProgressBar healthbar;
	
	private AnimatedSprite2D animator;

	[Export]
	public Inventory inventory;
	public override void _Ready()
	{
		base._Ready();
		health = 100;
		hungerBar = 100;
		damage = 20;
		canGetHit = false;
		isRunning = false;
		canAttack = true;
		attackCooldownTurnOn = false;
		isAttacking = false;
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		attackCooldownTimer = GetNode<Timer>("take_attack_cooldown");
		giveAttackCoolDownTimer = GetNode<Timer>("give_attack_cooldown");
		healthbar = GetNode<ProgressBar>("healthbar");
	}

	public override void _PhysicsProcess(double delta) {
		keyboardControl();
		mouseControl();
		setAnimation();
		checkForBeingHit();
		checkHealth();
		manageHealthBar();
		//GD.Print("is idle  " + isIdle);
	}

	public void keyboardControl() {
		if(!isAttacking) {
			setRunning(false);
		}
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

		if(Input.IsActionJustPressed("delete_item")) {
			inventory.deleteItem();
		}

		Position = position;
		animator.Scale = scale;
		//MoveAndSlide();
	}

	public void mouseControl() {
		if(Input.IsMouseButtonPressed(MouseButton.Left) && canAttack && !isAttacking) {
			setAttacking(true);
			canAttack = false;
			giveAttackCoolDownTimer.Start();
		}
	}

	private void _on_give_attack_cooldown_timeout(){
		setAttacking(false);
		canAttack = true;
	}
	
	public bool getAttacking() {
		return this.isAttacking; 
	}

	public int getDamage() {
		return this.damage;
	}

	public void setRunning(bool value) {
		isRunning = value;
		isIdle = !value;
	}

	public void setAttacking(bool value) {
		isAttacking = value;
		isIdle = !value;
	}

	public void setAnimation(){
		if(isAttacking) {
			animator.Play("attack");
		} else if (isIdle) {
			animator.Play("idle");
		} else if (isRunning) {
			animator.Play("run");
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
			//GD.Print(health);
		}

	}

	/*public void checkCollisionInventory(InventoryItem item) {
		switch(item.Name) {
			case "apple":
				inventory.addItem(item);
			break;
		}
	}*/

	public void addItemToInventory(InventoryItem item) {
		inventory.addItem(item);
	}

	private void _on_player_collision_body_entered(Node2D body) {
		/*switch (body.Name) {
			case "apple":
				GD.Print("Collision with the apple");
				//inventory.addItem(item);
			break;
		}*/
	}

	public void checkForBeingHit() {
		if (canGetHit) {
			switch(enemyType){
				case "SlimeEnemy":
					//dealDamage(new Enemy().getAttackDamage());
					//GD.Print("Player health is " + this.health);
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

	public void manageHealthBar() {
		healthbar.Value = health;
		//GD.Print(healthbar.Value);

		/*if(health >= 100) {
			healthbar.Visible = false;
		} else {
			healthbar.Visible = true;
		}*/

		if(health >= 100) {
			healthbar.Modulate = new Color("#75caa7");
		} else if (health <= 70 && health >= 30) {
			healthbar.Modulate = new Color("#e1aa6a");
		} else {
			healthbar.Modulate = new Color("#fe9296");
		}
	}


	public void die() {
		canGetHit = false;
		animator.Play("die");
		QueueFree();
	}

	public void addHealth(int howMuch) {
		this.health += howMuch;
	}
}

