using Godot;
using System;

public partial class EnemyAi : Node2D
{
	[Export]
	public Area2D PlayerDetectionZone { get; set; }

	[Export]
	public Enemy Enemy { get; set; }

	private bool attackPlayer = false;
	private Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerDetectionZone.BodyEntered += PlayerDetectionZone_BodyEntered;
		PlayerDetectionZone.BodyExited += PlayerDetectionZone_BodyExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (attackPlayer)
		{
			Enemy.Velocity = (player.Position - Enemy.Position).Normalized() * Enemy.MovementSpeed;
			Enemy.LookAt(player.Position);
			Enemy.MoveAndSlide();

			if(player.Position.DistanceSquaredTo(Enemy.Position) < 800)
			{
				player.DecreaseHealth();
				Enemy.QueueFree();
			}
		}
	}

	private void PlayerDetectionZone_BodyEntered(Node2D body)
	{
		var detectedBody = body as Player;
		if (detectedBody == null)
		{
			return;
		}
		player = detectedBody;
		attackPlayer = true;
	}

	private void PlayerDetectionZone_BodyExited(Node2D body)
	{
		var detectedBody = body as Player;
		if (detectedBody == null)
		{
			return;
		}
		player = detectedBody;
		attackPlayer = false;
	}
}
