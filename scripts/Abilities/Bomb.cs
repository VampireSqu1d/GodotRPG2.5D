using Godot;
using System;

public partial class Bomb : Ability {
    
    
    public override void _Ready() {
        
        animPlayerNode.AnimationFinished += HandleExpandAnimationFinished;
        //animPlayerNode.AnimationFinished += HandleExplosionAnimationFinished;
    }

    private void HandleExpandAnimationFinished(StringName animName) {
        if (animName == GameConstants.ANIM_EXPAND) {
            animPlayerNode.Play(GameConstants.ANIM_EXPLOSION);
        } else {
            QueueFree();
        }
    }

}
