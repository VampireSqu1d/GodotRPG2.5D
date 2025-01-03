using Godot;
using System;

public partial class Player : Character
{

    public override void _Ready() {
        base._Ready();

        GameEvents.OnReward += HandleReward;
    }

   


    public override void _Input(InputEvent @event) {
        direction = Input.GetVector(
			GameConstants.INPUT_MOVE_LEFT, 
			GameConstants.INPUT_MOVE_RIGHT, 
			GameConstants.INPUT_MOVE_FORWARD, 
			GameConstants.INPUT_MOVE_BACKWARD
			);

    }

    public void CheckForAttackInput() {
        
    }
	

    private void HandleReward(RewardResource resource) {
        StatResource targetStat = GetStatResourse(resource.TargetStat);
        targetStat.StatValue += resource.Amount;
    }
}
