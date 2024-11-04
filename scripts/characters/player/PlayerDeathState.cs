using Godot;
using System;

public partial class PlayerDeathState : PlayerState {

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimPlayer.AnimationFinished += HandleAnimationFinished;
        
    }

    private void HandleAnimationFinished(StringName animName) {
        GameEvents.RaiseEndGame();
        characterNode.QueueFree();

    }

}
