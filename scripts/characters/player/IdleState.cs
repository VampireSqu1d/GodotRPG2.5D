using Godot;
using System;


public partial class IdleState : PlayerState
{
    

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        if (characterNode.direction != Vector2.Zero) {
            characterNode.StateMachineNode.SwitchState<MoveState>();
        }
    }

    

    public override void _Input(InputEvent @event) {
        

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            characterNode.StateMachineNode.SwitchState<DashState>();
        }
    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
