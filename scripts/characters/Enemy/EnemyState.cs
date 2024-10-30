using System;
using Godot;

public abstract partial class EnemyState: CharacterState {
    protected Vector3 destination;

    public override void _Ready() {
        base._Ready();

        characterNode.GetStatResourse(Stat.Health).OnZero += HandleZeroHealth;
    }

    public Vector3 GetPointGlobalPosition(int idx) {
        Vector3 localPos = characterNode.PathNode.Curve.GetPointPosition(idx);
        Vector3 globalPos = characterNode.PathNode.GlobalPosition;

        return globalPos + localPos;
    }   

    public void Move(){
        characterNode.AgentNode.GetNextPathPosition();
        characterNode.Velocity = 2*characterNode.GlobalPosition.DirectionTo(destination);
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected void HandleChaseAreaNodeEntered(Node3D body) {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth() {
        characterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }
}