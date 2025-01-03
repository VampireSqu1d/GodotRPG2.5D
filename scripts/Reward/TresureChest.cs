using Godot;
using System;

public partial class TresureChest : StaticBody3D {
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;
    [Export] private RewardResource reward;

    public override void _Ready() {
        areaNode.BodyEntered += (body) => spriteNode.Visible = true;

        areaNode.BodyExited += (body) => spriteNode.Visible = false;

        spriteNode.Visible = false;
    }

    public override void _Input(InputEvent @event) {
        if (
            !areaNode.Monitoring ||
            !areaNode.HasOverlappingBodies() || 
            !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)) {
            return;
        }

        areaNode.Monitoring = false;

        GameEvents.RaiseReward(reward);
        GD.Print("interacting with chest");
    }
}
