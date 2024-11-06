using Godot;
using System;

public partial class Lightning : Ability {
    

    public override void _Ready() {
        
        animPlayerNode.AnimationFinished += (anim) => QueueFree();
        
    }

    private void HandleAnimationFinished(StringName animName) {
        throw new NotImplementedException();
    }

}
