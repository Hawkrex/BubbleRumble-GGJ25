using Godot;

public partial class EnemyGenerator : Node
{
	public readonly PackedScene Enemy = ResourceLoader.Load<PackedScene>("res://Scenes/enemy.tscn");

	[Signal]
	public delegate void EnemyCreatedEventHandler(Enemy enemy);

	[Export]
	public float EnemyPerMinute { get; set; } = 60.0f;

	private float spawnRate;

	private float spawnTime = 0;

	public override void _Ready()
	{
		base._Ready();

		spawnRate = 60 / EnemyPerMinute;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (spawnTime > spawnRate)
		{
			SpawnEnemy();
			spawnTime = 0;
		}
		else
		{
			spawnTime += (float)delta;
		}
	}

	private void SpawnEnemy()
	{
		var rng = new RandomNumberGenerator();

		var children = GetChildren();

		var position = (children[(int)(rng.Randi() % children.Count)] as Node2D).GlobalPosition;

		var enemyInstance = Enemy.Instantiate() as Enemy;

		enemyInstance.GlobalPosition = position;

		GetTree().Root.AddChild(enemyInstance);

		EmitSignal(SignalName.EnemyCreated, enemyInstance);
	}
}
