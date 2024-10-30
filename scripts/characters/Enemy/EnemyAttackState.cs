using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState {

    private Vector3 targetPosition;

    public override void _Ready() {
        base._Ready();

    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_ATTACK);

        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();

        targetPosition = target.GlobalPosition;

        characterNode.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState() {
        characterNode.AnimPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName) {
        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null) {
            Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();

            if (chaseTarget == null) {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;    
            }
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }

        characterNode.AnimPlayer.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 diretion = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.SpriteNode.FlipH = diretion.X < 0;

        characterNode.ToggleHitBox(true);
    }

    private void PerformHit() {
        
        characterNode.HitBoxNode.GlobalPosition = targetPosition;

        characterNode.ToggleHitBox(false);
    }
}
