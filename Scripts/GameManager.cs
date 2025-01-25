using Godot;

public partial class GameManager : Node2D
{
	[Signal]
	public delegate void ScoreChangedEventHandler(double score);

	public double Score { get; internal set; } = 0;

	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		player.GameOver += GameOver;

		var enemyGenerator = GetNode<EnemyGenerator>("EnemyGenerator");
		enemyGenerator.EnemyCreated += EnemyCreatedHandler;
	}

	private void GameOver()
	{
	}

	private void EnemyCreatedHandler(Enemy enemy)
	{
		enemy.EnemyDead += EnemyDeadHandler;
	}

	private void EnemyDeadHandler(Enemy enemy, int score)
	{
		Score += score;
		EmitSignal(SignalName.ScoreChanged, Score);
	}
}
