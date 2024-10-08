using Godot;
using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Tasks.Dataflow;
public partial class Char : CharacterBody2D
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
	public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;

	private void moveSpriteHandler (Vector2 direction, Vector2 velocity, AnimatedSprite2D playerSprite)
	{
		if (direction.X > 0 && playerSprite.FlipH == true)
			playerSprite.FlipH = false;
		else if (direction.X < 0 && playerSprite.FlipH == false)
			playerSprite.FlipH = true;
		if (IsOnFloor())
			playerSprite.Animation = "move";
	}
	public override void _PhysicsProcess(double delta)
	{
		AnimatedSprite2D playerSprite = GetNode<AnimatedSprite2D>("playerSprite");
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			playerSprite.Animation = "jump";
			if (velocity.Y < 0 && velocity.Y != -340)
				playerSprite.Frame = 0;
			else if (velocity.Y == -340)
				playerSprite.Frame = 1;
			else
				playerSprite.Frame = 2;
		}
		// Handle Jump.
		if (Input.IsActionPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

	/* 	if (Input.IsKeyPressed(Key.Down) && direction.X == 0)
		{
			playerSprite.Animation = "crounch";
			playerSprite.Frame = 0;
			playerSprite.Frame = 1;
			playerSprite.Frame = 2;
		} */
		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
			moveSpriteHandler(direction, velocity, playerSprite);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			playerSprite.Play("default");
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
