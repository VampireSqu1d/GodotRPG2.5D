using Godot;
using System;

public partial class StateMachine : Node
{
	[Export] private Node currenState;
	[Export] private Node[] states;

    public override void _Ready() {
        currenState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
		
    }

	public void SwitchState<T>() {
		Node newState = null;

		foreach (Node state in states) {
			if (state is T) {
				newState = state;
			}
		}

		if (newState == null) {
			return;
		}

		currenState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
		currenState = newState;
		currenState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
	}
}
