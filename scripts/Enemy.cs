using Godot;

public partial class Enemy : RigidBody2D
{
	/* 	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey inputKey)
		{
			Key keyCode = inputKey.Keycode;
			GD.Print($"Key code: {keyCode}");
			// Handle key press or release as needed
		}
	}*/
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}
}
