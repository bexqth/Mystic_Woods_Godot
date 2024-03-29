using Godot;
using Godot.NativeInterop;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection.Metadata;

public partial class Player : CharacterBody2D
{
	private  float speed = 5.0f;
	private bool isIdle;
	private bool isRunning;
	private bool isAttacking;
	private bool isDead;
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
	private Timer axeCoolDownTimer;
	private Timer pickaxeCoolDownTimer;
	private bool isWatering;
	private bool isHoeing;
	private bool isPlanting;
	private bool isAxing;
	private bool isPickaxing;
	private bool canWater;
	private bool canPlant;
	private bool canHoe;
	private bool canAxe;
	private bool canPickaxe;
	private String direction;

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
		waterCoolDownTimer = GetNode<Timer>("water_cooldown");
		hoeCoolDownTimer = GetNode<Timer>("hoe_cooldown");
		plantCoolDownTimer = GetNode<Timer>("plant_cooldown");
		axeCoolDownTimer = GetNode<Timer>("axe_cooldown");
		pickaxeCoolDownTimer = GetNode<Timer>("pickaxe_cooldown");

		isWatering = false;
		isHoeing = false;
		isPlanting = false;
		isAxing = false;
		isPickaxing = false;
		canWater = true;
		canHoe = true;
		canPlant = true;
		canAxe = true;
		canPickaxe = true;
		isIdle = true;
		direction = "down";
	}

	public override void _Process(double delta)
	{
		//changeCollision();
	}

	public override void _PhysicsProcess(double delta) {
		keyboardControl();
		mouseControl();
		changeDirectionOnMouse();
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
		Godot.Vector2 position = Position;
		//Godot.Vector2 scale = animator.Scale;

		if(Input.IsActionPressed("pressed_a")) {
			position.X -= speed;
			//scale.X = -1;
			setRunning(true);
			direction = "left";
		} else if(Input.IsActionPressed("pressed_d")) {
			position.X += speed;
			//scale.X = 1;
			setRunning(true);
			direction = "right";
		} else if (Input.IsActionPressed("pressed_w")) {
			position.Y -= speed;
			setRunning(true);
			direction = "up";
		} else if(Input.IsActionPressed("pressed_s")) {
			position.Y += speed;	
			setRunning(true);
			direction = "down";
		}

		if(Input.IsActionJustPressed("pressed_q")) {
			inventory.deleteItem();
		}
		//GD.Print(direction);

		Position = position;
		world.setPlayerPosition(this.Position);
		//animator.Scale = scale;
		MoveAndSlide();
	}

	public void mouseControl() {
		holdingItem = inventory.getHoldingItem();
		if(Input.IsActionJustPressed("on_left_click") && holdingItem != null && !world.getPlayerOpenedChest() && !world.getPlayerUsingTable()) {
			holdingItem.useItem();
		}
	}

	public Godot.Vector2 globalMousePosition() {
		Godot.Vector2 mousePosition = GetGlobalMousePosition();
		int mouseX = (int)(mousePosition.X / 3);
		int mouseY = (int)(mousePosition.Y / 3);
		Godot.Vector2 newMousePosition = new Godot.Vector2(mouseX, mouseY);
		return newMousePosition;
	}

	public InventoryItem getHoldingItem() {
		return this.holdingItem;
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

	public bool getCanAxe() {
		return this.canAxe;
	}

	public void setCanAxe(bool b) {
		canAxe = b;
	}

	public bool getCanPickaxe() {
		return this.canPickaxe;
	}

	public void setCanPickaxe(bool b) {
		canPickaxe = b;
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

	public Timer getAxeCoolDownTimer() {
		return axeCoolDownTimer;
	}

	public Timer getPickaxeCoolDownTimer() {
		return pickaxeCoolDownTimer;
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

	public bool getAxing() {
		return this.isAxing;
	}

	public bool getPickaxing() {
		return this.isPickaxing; 
	}

	public int getDamage() {
		return this.damage;
	}

	public void setPickaxing(bool value) {
		isPickaxing = value;
		isIdle = !value;
	}

	public void setAxing(bool value) {
		isAxing = value;
		isIdle = !value;
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
			if(direction == "up") {
				animator.Play("attack_up");
			} else if(direction == "down") {
				animator.Play("attack_down");
			} else if(direction == "left") {
				animator.Play("attack_left");
			} else if(direction == "right"){
				animator.Play("attack_right");
			}
		} else if(isWatering) {
			if(direction == "up") {
				animator.Play("water_up");
			} else if(direction == "down") {
				animator.Play("water_down");
			} else if(direction == "left") {
				animator.Play("water_left");
			} else if(direction == "right"){
				animator.Play("water_right");
			}
		} else if(isHoeing) {
			if(direction == "up") {
				animator.Play("hoe_up");
			} else if(direction == "down") {
				animator.Play("hoe_down");
			} else if(direction == "left") {
				animator.Play("hoe_left");
			} else if(direction == "right"){
				animator.Play("hoe_right");
			}
		} else if(isPlanting) {
			if(direction == "up") {
				animator.Play("plant_up");
			} else if(direction == "down") {
				animator.Play("plant_down");
			} else if(direction == "left") {
				animator.Play("plant_left");
			} else if(direction == "right"){
				animator.Play("plant_right");
			}
		} else if(isAxing) {
			if(direction == "up") {
				animator.Play("axe_up");
			} else if(direction == "down") {
				animator.Play("axe_down");
			} else if(direction == "left") {
				animator.Play("axe_left");
			} else if(direction == "right"){
				animator.Play("axe_right");
			}
		} else if(isPickaxing) {
			if(direction == "up") {
				animator.Play("pickaxe_up");
			} else if(direction == "down") {
				animator.Play("pickaxe_down");
			} else if(direction == "left") {
				animator.Play("pickaxe_left");
			} else if(direction == "right"){
				animator.Play("pickaxe_right");
			} 
		} else if (isIdle) {
			if(direction == "up") {
				animator.Play("idle_up");
			} else if(direction == "down") {
				animator.Play("idle_down");
			} else if(direction == "left") {
				animator.Play("idle_left");
			} else if(direction == "right"){
				animator.Play("idle_right");
			}
		} else if (isRunning) {
			if(direction == "up") {
				animator.Play("run_up");
			} else if(direction == "down") {
				animator.Play("run_down");
			} else if(direction == "left") {
				animator.Play("run_left");
			} else if(direction == "right"){
				animator.Play("run_right");
			}
		}
	}

	public void changeDirectionOnMouse() {
		
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

	private void _on_axe_cooldown_timeout()
	{
		setAxing(false);
		canAxe = true;
	}
	
	private void _on_pickaxe_cooldown_timeout()
	{
		setPickaxing(false);
		canPickaxe = true;
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

	public void addItemToInventory(InventoryItem item) {
		inventory.addItem(item);
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

	private void _on_player_collision_body_entered(Node2D body)
	{
		/*GD.Print(body.GetType());
		if(body.GetParent() is InventoryItem newItem) {
			//this.inventory.addItem(newItem);
			GD.Print("Collision with inventory item");
		}*/
	}

	public bool axeAnimationEnd() {
		if(this.isAxing && this.animator.Frame ==  4) {
			return true;
		}
		return false; 
	}

	public bool pickaxeAnimationEnd() {
		if(this.isPickaxing && this.animator.Frame ==  4) {
			return true;
		}
		return false; 
	}

	public AnimatedSprite2D getAnimator() {
		return this.animator;
	}

	private void _on_player_collision_area_entered(Area2D area)
	{
		if(area.GetParent() is InventoryItem newItem) {
			this.addItemToInventory(newItem);
		}
	}

}

