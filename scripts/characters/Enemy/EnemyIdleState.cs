using Godot;

public partial class EnemyIdleState : EnemyState {


    public override void _PhysicsProcess(double delta) {
            characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_IDLE);
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaNodeEntered;
    }

    protected override void ExitState() {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaNodeEntered;
    }
}