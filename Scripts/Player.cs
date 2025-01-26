using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Health { get; set; } = 3;

	[Export]
	public float MovementSpeed { get; set; } = 300.0f;

	[Export]
	public float FireRate { get; set; } = 0.5f;

	[Export]
	public double InvulnerabilityTimer { get; set; } = 3;

	[Signal]
	public delegate void GameOverEventHandler();

	[Signal]
	public delegate void HealthChangedEventHandler(int health);

	public readonly PackedScene Bullet = ResourceLoader.Load<PackedScene>("res://Scenes/bullet.tscn");

	public Node2D CanonEnd;

	private bool canFire = true;
	private double timer = 0;
	private double invulnerabilityTimer = 0;

	public override void _Ready()
	{
		CanonEnd = GetNode<Node2D>("CanonEnd");
	}

	public override void _PhysicsProcess(double delta)
	{
		MovePlayer();
		RotatePlayer();

		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (!canFire)
		{
			timer += delta;

			if (timer > FireRate)
			{
				canFire = true;
				timer = 0;
			}
		}

		if (Input.IsActionPressed("Shoot") && canFire)
		{
			var bulletInstance = Bullet.Instantiate() as CharacterBody2D;

			bulletInstance.Position = CanonEnd.GlobalPosition;
			bulletInstance.GlobalRotation = GlobalRotation - Mathf.Pi / 2;

			GetTree().Root.AddChild(bulletInstance);

			canFire = false;
		}

		if (invulnerabilityTimer > 0)
		{
			invulnerabilityTimer -= delta;
		}
	}

	public void DecreaseHealth()
	{
		if (invulnerabilityTimer <= 0)
		{
			Health--;
			invulnerabilityTimer = InvulnerabilityTimer;

			EmitSignal(SignalName.HealthChanged, Health);

			if (Health <= 0)
			{
				EmitSignal(SignalName.GameOver);
			}
		}
	}

	private void MovePlayer()
	{
		var movementVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		Velocity = movementVector * MovementSpeed;
	}

	private void RotatePlayer()
	{
		var joystickVector = new Vector2(Input.GetAxis("LookLeft", "LookRight"), Input.GetAxis("LookDown", "LookUp"));

		if (joystickVector.X != 0 || joystickVector.Y != 0)
		{
			Rotation = Mathf.Atan2(joystickVector.X, joystickVector.Y);
		}
	}
}
