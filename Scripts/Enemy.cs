using Godot;

public partial class Enemy : RigidBody2D
{
	[Signal]
	public delegate void EnemyDeadEventHandler(Enemy enemy, int score);

	public void DecreaseHealth()
	{
		EmitSignal(SignalName.EnemyDead, this, 100);

		QueueFree();
	}
}
