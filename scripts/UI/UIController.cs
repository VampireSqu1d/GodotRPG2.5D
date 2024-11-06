using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control {
    private Dictionary<ContainerType, UIContainer> containers;

    private bool canPause = false;

    public override void _Ready() {
        containers = GetChildren()
            .Where( (element) => element is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary((element) => element.container);
        
        containers[ContainerType.Start].Visible = true;

        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;

        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
        GameEvents.OnReward += HandleReward;
    }


    public override void _Input(InputEvent @event) {

        if(!canPause) {
            return;
        }

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE)) {
            return;
        }

        if (containers[ContainerType.Pause].Visible) {
            HandlePausePressed();
            return;
        }

        containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleVictory() {
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;

    }


    private void HandleEndGame() {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }


    private void HandleStartPressed() {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
    }

    private void HandlePausePressed() {
        containers[ContainerType.Stats].Visible = true;
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
    }

    private void HandleRewardPressed() {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Reward].Visible = false;
    }

    private void HandleReward(RewardResource resource) {
        canPause = false;
        GetTree().Paused = true;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Reward].Visible = true;

        containers[ContainerType.Reward].textureNode.Texture = resource.spriteTexture;
        containers[ContainerType.Reward].labelNode.Text = resource.Description;
    }


}
