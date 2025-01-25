using Godot;

public partial class Bullet : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 10.0f;

	private Vector2 motion;

	public override void _Ready()
	{
		base._Ready();

		motion = new Vector2(Speed, 0).Rotated(Rotation);
	}

	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(motion);

		if(collision != null)
		{
			QueueFree();
		}
	}
}
