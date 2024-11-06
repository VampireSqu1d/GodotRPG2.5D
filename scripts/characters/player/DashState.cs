using Godot;
using System;

public partial class DashState : PlayerState
{
	[Export] private Timer dashTimer;
	[Export] private PackedScene bombScene;
	[Export(PropertyHint.Range, "1, 20, 0.1")] private float dash_speed = 10;

    public override void _Ready() {
        base._Ready();
		dashTimer.Timeout += handleDashTimeout;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
		characterNode.MoveAndSlide();
		characterNode.Flip();
    }

    protected override void EnterState() {
		characterNode.AnimPlayer.Play(GameConstants.ANIM_DASH);
		characterNode.Velocity = new( characterNode.direction.X, 0, characterNode.direction.Y);

		if (characterNode.Velocity == Vector3.Zero) {
			characterNode.Velocity = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
		}

		characterNode.Velocity *= dash_speed;
		dashTimer.Start();

		Node3D bomb = bombScene.Instantiate<Node3D>();
		GetTree().CurrentScene.AddChild(bomb);
		bomb.GlobalPosition = characterNode.GlobalPosition;
    }

	private void handleDashTimeout(){
		characterNode.Velocity = Vector3.Zero;
		characterNode.StateMachineNode.SwitchState<IdleState>();
	}
}
