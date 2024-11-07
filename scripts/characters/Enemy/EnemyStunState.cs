using Godot;
using System;

public partial class EnemyStunState : EnemyState {

    protected override void EnterState(){
        base.EnterState();

        characterNode.AnimPlayer.Play(GameConstants.ANIM_STUN);

        characterNode.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState() {
        base.ExitState();
        characterNode.AnimPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName) {
        if(characterNode.AttackAreaNode.HasOverlappingBodies()) {
            characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
        } else if (characterNode.ChaseAreaNode.HasOverlappingBodies()) {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        } else {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }


    }

}
