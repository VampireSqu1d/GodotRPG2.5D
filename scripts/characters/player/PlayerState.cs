using Godot;
using System;

public abstract partial class PlayerState : CharacterState {

    public override void _Ready() {
        base._Ready();

        characterNode.GetStatResourse(Stat.Health).OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth() {
        characterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }

    protected void CheckForAttackInput() {
        if(Input.IsActionJustPressed(GameConstants.INPUT_ATTACK)) {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
    
}