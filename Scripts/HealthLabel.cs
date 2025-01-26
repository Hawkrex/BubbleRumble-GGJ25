using Godot;

public partial class HealthLabel : Label
{
	[Export]
	public GameManager Node { get; set; }
	
	public override void _Ready()
	{
		base._Ready();

		var player = Node.GetNode<Player>("Player");

		player.HealthChanged += HealthChangedHandler;
	}

	private void HealthChangedHandler(int health)
	{
		Text = $"Health : {health}";
	}
}
