using Godot;

public partial class Enemy : CharacterBody2D
{
	[Signal]
	public delegate void EnemyDeadEventHandler(Enemy enemy, int score);

	[Export]
	public int Health { get; set; } = 1;

	[Export]
	public float MovementSpeed { get; set; } = 100f;

	[Export]
	public float FireRate { get; set; } = 1f;

	public void DecreaseHealth()
	{
		EmitSignal(SignalName.EnemyDead, this, 100);

		QueueFree();
	}
}
