using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState {

    [Export] private Timer TimerNode;
    private CharacterBody3D target;


    public override void _PhysicsProcess(double delta) {
        Move();
    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;

        TimerNode.Timeout += HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState() {
        TimerNode.Timeout -= HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleAttackAreaBodyEntered(Node3D body) {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleTimeout() {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }

      private void HandleChaseAreaBodyExited(Node3D body) {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

}
