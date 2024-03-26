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
	private bool waterCooldownTurnOn;
	private bool hoeCooldownTurnOn;
	private bool plantCooldownTurnOn;
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
	private World world;
	private AnimationTree animationTree;
	private Timer waterCoolDownTimer;
	private Timer hoeCoolDownTimer;
	private Timer plantCoolDownTimer;
	private bool isWatering;
	private bool isHoeing;
	private bool isPlanting;
	private bool canWater;
	private bool canPlant;
	private bool canHoe;

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
		world = (World)GetNode("/root/World");
		isIdle = true;
		waterCoolDownTimer = GetNode<Timer>("water_cooldown");
		hoeCoolDownTimer = GetNode<Timer>("hoe_cooldown");
		plantCoolDownTimer = GetNode<Timer>("plant_cooldown");
		isWatering = false;
		isHoeing = false;
		isPlanting = false;
		canWater = true;
		canHoe = true;
		canPlant = true;
	}

	public override void _Process(double delta)
	{
		//changeCollision();
	}

	public override void _PhysicsProcess(double delta) {
		keyboardControl();
		mouseControl();
		setAnimation();
		checkForBeingHit();
		checkHealth();
		manageHealthBar();
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

		if(Input.IsActionJustPressed("pressed_q")) {
			inventory.deleteItem();
		}

		Position = position;
		animator.Scale = scale;
		MoveAndSlide();
	}

	public void mouseControl() {
		holdingItem = inventory.getHoldingItem();
		if(Input.IsActionJustPressed("on_left_click") && holdingItem != null && !world.getPlayerOpenedChest()) {
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

	public void setCanWater(bool b) {
		canWater = b;
	}

	public bool getCanWater() {
		return canWater;
	}

	public void setCanHoe(bool b) {
		canHoe = b;
	}

	public bool getCanHoe() {
		return canHoe;
	}

	public void setCanPlant(bool b) {
		canPlant = b;
	}

	public bool getCanPlant() {
		return canPlant;
	}

	public Timer getGiveAttackCoolDownTimer() {
		return giveAttackCoolDownTimer;
	}

	public Timer getWaterCoolDownTimer() {
		return waterCoolDownTimer;
	}

	public Timer getHoeCoolDownTimer() {
		return hoeCoolDownTimer;
	}

	public Timer getPlantCoolDownTimer() {
		return plantCoolDownTimer;
	}
	
	public bool getAttacking() {
		return this.isAttacking; 
	}

	public bool getWatering() {
		return this.isWatering; 
	}

	public bool getHoeing() {
		return this.isHoeing; 
	}

	public bool getPlanting() {
		return this.isPlanting; 
	}

	public int getDamage() {
		return this.damage;
	}

	public void setWatering(bool value) {
		isWatering = value;
		isIdle = !value;
	}

	public void setHoeing(bool value) {
		isHoeing = value;
		isIdle = !value;
	}

	public void setPlanting(bool value) {
		isPlanting = value;
		isIdle = !value;
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
		} else if(isWatering) {
			animator.Play("water");
		} else if(isHoeing) {
			animator.Play("hoe");
		} else if(isPlanting) {
			animator.Play("plant");
		} else if (isIdle) {
			animator.Play("idle");
		} else if (isRunning) {
			animator.Play("run");
		}
	}

	private void _on_water_cooldown_timeout()
	{
		setWatering(false);
		canWater = true;
	}


	private void _on_hoe_cooldown_timeout()
	{
		setHoeing(false);
		canHoe = true;
	}


	private void _on_plant_cooldown_timeout()
	{
		setPlanting(false);
		canPlant = true;
	}
	
	private void _on_give_attack_cooldown_timeout(){
		setAttacking(false);
		canAttack = true;
	}

	public void dealDamage(int damage) {
		if (!attackCooldownTurnOn) {
			health -= damage;
			attackCooldownTurnOn = true;
			attackCooldownTimer.Start();
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

	public void water() {

	}

	public void plant() {

	}

	public void hoe() {

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

