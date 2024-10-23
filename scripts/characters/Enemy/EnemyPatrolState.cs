using Godot;
using System;

public partial class EnemyPatrolState : EnemyState {
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float maxIdleTime = 4;
    private int pointIdx = 0;

    public override void _PhysicsProcess(double delta) {
        if (!idleTimerNode.IsStopped()) {
            return;
        }
        Move();
    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        pointIdx = 1;

        destination = GetPointGlobalPosition(pointIdx);
        characterNode.AgentNode.TargetPosition = destination;
        // subscribe to signals
        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaNodeEntered;
        idleTimerNode.Timeout += HandleTimeout;
    }

    protected override void ExitState() {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaNodeEntered;
    }

    private void HandleTimeout() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        pointIdx = Mathf.Wrap(pointIdx + 1, 0, characterNode.PathNode.Curve.PointCount);

        destination = GetPointGlobalPosition(pointIdx);
        characterNode.AgentNode.TargetPosition = destination;
    }

    private void HandleNavigationFinished() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_IDLE);
        
        RandomNumberGenerator rng = new();
        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);
        idleTimerNode.Start();

        pointIdx = Mathf.Wrap(pointIdx + 1, 0, characterNode.PathNode.Curve.PointCount);
        destination = GetPointGlobalPosition(pointIdx);
        characterNode.AgentNode.TargetPosition = destination;
    }

}
