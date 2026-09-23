using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	public bool doubleJump = true;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int hp_value = 3;
	public bool ujra_eled = false;
	
	private async void CameraShake() {
		Camera2D Camera = GetTree().CurrentScene.GetNode<Camera2D>("Camera2D");
		for (short i = 0; i < 5; ++i) {
			Camera.Offset = new Vector2(5, 0);
			await ToSignal(GetTree().CreateTimer(0.06), SceneTreeTimer.SignalName.Timeout);
			Camera.Offset = new Vector2(-5, 0);
			await ToSignal(GetTree().CreateTimer(0.06), SceneTreeTimer.SignalName.Timeout);
		}
	}
	
	public async override void _PhysicsProcess(double delta) {
		if (ujra_eled) return;
		
		Node NodeHP = GetTree().CurrentScene.GetNode<Node>("HP");
		Vector2 ujra_eledesi_pont = GetTree().CurrentScene.GetNode<Node2D>("Újraéledés").Position;
		
		// éledjen újra
		if (Position.Y > 700) {
			ujra_eled = true;
			hp_value -= 1;
			if (hp_value > -1) {
				NodeHP.GetNode<TextureRect>($"{hp_value+1}").Visible = false;
			}
			CameraShake();
			await ToSignal(GetTree().CreateTimer(1.45), SceneTreeTimer.SignalName.Timeout);
			Position = ujra_eledesi_pont;
			ujra_eled = false;
		}
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		} else {
			doubleJump = true;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept"))
		{
			if (IsOnFloor()) {
				velocity.Y = JumpVelocity;
			} else if (doubleJump) {
				doubleJump = false;
				velocity.Y = JumpVelocity;
			}
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
