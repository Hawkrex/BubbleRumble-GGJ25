using Godot;

public partial class ScoreLabel : Label
{
	[Export]
	public GameManager Node { get; set; }

	public override void _Ready()
	{
		base._Ready();

		//var game = GetNode<Node2D>("Game") as GameManager;
		Node.ScoreChanged += ScoreChangedHandler;
	}

	private void ScoreChangedHandler(double score)
	{
		Text = $"Score : {score}";
	}
}
