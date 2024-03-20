using Godot;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

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
	private InventoryItem holdingItem;

	private String farmingTool;
	private bool wantsToPlantSeed;

	private FarmingManager farmingManager;

	[Export]
	public Inventory inventory;
	//private TileMap tileMap;
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
		//tileMap = GetNode<TileMap>("TileMap");
		//tileMap = GetNode<TileMap>("../TileMap");
		isIdle = true;
	}

	public override void _PhysicsProcess(double delta) {
		keyboardControl();
		mouseControl();
		setAnimation();
		checkForBeingHit();
		checkHealth();
		manageHealthBar();
		//GD.Print(Position.X + ", " + Position.Y);
	}

	public void setFarmingTool(String s) {
		farmingTool = s;
	}

	public String getFarmingTool() {
		return farmingTool;
	}

	public bool getWantsToPlantSeed() {
		return wantsToPlantSeed;
	}

	public void setWantsToPlantSeed(bool b) {
		this.wantsToPlantSeed = b;
	}

	public void keyboardControl() {
		if(!isAttacking) {
			setRunning(false);
		}
		Vector2 position = Position;
		Vector2 scale = animator.Scale;

		if(Input.IsActionPressed("pressed_a")) {
			position.X -= speed;
			scale.X = -1;
			setRunning(true);	
		} else if(Input.IsActionPressed("pressed_d")) {
			position.X += speed;
			scale.X = 1;
			setRunning(true);
		} else if (Input.IsActionPressed("pressed_w")) {
			position.Y -= speed;
			setRunning(true);
		} else if(Input.IsActionPressed("pressed_s")) {
			position.Y += speed;	
			setRunning(true);
		}

		if(Input.IsActionJustPressed("delete_item")) { //Prepisat na Q
			inventory.deleteItem();
		}

		Position = position;
		animator.Scale = scale;
		//MoveAndSlide();
	}

	public void mouseControl() {
		holdingItem = inventory.getHoldingItem();

		if(Input.IsActionJustPressed("on_left_click") && holdingItem != null) {
			holdingItem.useItem();
		}
	}


	public Vector2 globalMousePosition() {
		Vector2 mousePosition = GetGlobalMousePosition();
		int mouseX = (int)(mousePosition.X / 3);
		int mouseY = (int)(mousePosition.Y / 3);
		Vector2 newMousePosition = new Vector2(mouseX, mouseY);
		return newMousePosition;
	}

	public String getNameHoldingItem() {
		if(holdingItem != null) {
			return holdingItem.getItemName();
		}
		return null;
	}

	public void setCanAttack(bool b) {
		canAttack = b;
	}

	public bool getCanAttack() {
		return canAttack;
	}

	public Timer getGiveAttackCoolDownTimer() {
		return giveAttackCoolDownTimer;
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
		}

	}

	public void addItemToInventory(InventoryItem item) {
		inventory.addItem(item);
	}

	private void _on_player_collision_body_entered(Node2D body) {
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

