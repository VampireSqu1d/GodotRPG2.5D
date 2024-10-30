using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	[Export] private Node currenState;
	[Export] private Node[] states;

    public override void _Ready() {
        currenState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
		
    }

	public void SwitchState<T>() {
		// Node newState = null;
		// foreach (Node state in states) {
		// 	if (state is T) {
		// 		newState = state;
		// 	}
		// }
		Node newState = states.Where((state) => state is T).FirstOrDefault();

		if (newState == null) {	return;	}

		if (currenState is T) { return; }
		
		currenState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
		currenState = newState;

		GD.Print(currenState.Name);

		currenState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
	}
}
