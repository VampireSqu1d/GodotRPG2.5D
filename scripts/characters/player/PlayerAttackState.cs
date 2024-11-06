using Godot;
using System;

public partial class PlayerAttackState : PlayerState {
    
    [Export] private Timer comboTimerNode;
    [Export] private PackedScene LightningScene;

    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready() {
        base._Ready();

        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState() {
        characterNode.AnimPlayer.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f);
        characterNode.AnimPlayer.AnimationFinished += HandleAnimFinished;
        characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;
    }

    protected override void ExitState() {
        characterNode.AnimPlayer.AnimationFinished -= HandleAnimFinished;
        comboTimerNode.Start();
    }

    private void HandleAnimFinished(StringName animName) {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);

        characterNode.ToggleHitBox(true);
        
        characterNode.StateMachineNode.SwitchState<IdleState>();
    }


    private void PerformHit() {
        Vector3 newPos = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        float distanceMultiplier = 0.80f;
        characterNode.HitBoxNode.Position = newPos * distanceMultiplier;

        characterNode.ToggleHitBox(false);
    }

    private void HandleBodyEntered(Node3D body) {
        if(comboCounter != maxComboCount) return;

        Node3D lightningInst = LightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightningInst);
        lightningInst.GlobalPosition = body.GlobalPosition;
    }
}
