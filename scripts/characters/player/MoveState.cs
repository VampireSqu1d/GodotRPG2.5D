using Godot;
using System;

public partial class MoveState : PlayerState
{
    [Export(PropertyHint.Range, "1, 20, .1")] private float speed = 5;


	public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        if (characterNode.direction == Vector2.Zero) {
            characterNode.StateMachineNode.SwitchState<IdleState>();
			return;
        }

		characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
		characterNode.Velocity *= speed;
		
		characterNode.MoveAndSlide();

		characterNode.Flip();
    }

	public override void _Input(InputEvent @event) {
        CheckForAttackInput();
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            characterNode.StateMachineNode.SwitchState<DashState>();
        }
    }

    protected override void EnterState() {
		characterNode.AnimPlayer.Play(GameConstants.ANIM_MOVE);
    }
}
