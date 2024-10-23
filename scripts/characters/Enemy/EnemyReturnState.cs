using Godot;
using System;

public partial class EnemyReturnState : EnemyState {


    public override void _Ready() {
        base._Ready();
        destination = GetPointGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta) {
        
        
        if (characterNode.AgentNode.IsNavigationFinished()) {
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
        }
        
        Move();

    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        characterNode.AgentNode.TargetPosition = destination;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaNodeEntered;
    }

    protected override void ExitState() {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaNodeEntered;
    }
}   

