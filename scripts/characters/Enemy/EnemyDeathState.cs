using Godot;
using System;

public partial class EnemyDeathState : EnemyState {

    protected override void EnterState() {
        base.EnterState();
        characterNode.AnimPlayer.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName) {
        characterNode.PathNode.QueueFree();
        //characterNode.QueueFree();
    }
}
