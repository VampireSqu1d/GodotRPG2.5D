using Godot;
using System;
using System.Linq;

public abstract partial class Character : CharacterBody3D
{

	[Export] private StatResource[] stats;


	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimPlayer {get; private set;}
	[Export] public Sprite3D SpriteNode {get; private set;}
	[Export] public StateMachine StateMachineNode {get; private set;}
	[Export] public Area3D HurtBoxNode {get; private set;}
	[Export] public Area3D HitBoxNode {get; private set;}
	[Export] public CollisionShape3D HitBoxShapeNode {get; private set;}

	[ExportGroup("AI Nodes")]
	[Export] public Path3D PathNode { get; private set;}
	[Export] public NavigationAgent3D AgentNode {get; private set;}
	[Export] public Area3D ChaseAreaNode {get; private set;}
	[Export] public Area3D AttackAreaNode {get; private set;}

    public Vector2 direction = new();

    public override void _Ready() {
        HurtBoxNode.AreaEntered += HandleHurtboxEntered;
    }

    private void HandleHurtboxEntered(Area3D area) {
        StatResource health = GetStatResourse(Stat.Health);
		
		Character player = area.GetOwner<Character>();
		
		health.StatValue -= player.GetStatResourse(Stat.Strength).StatValue;

		GD.Print(health.StatValue);
    }

    public StatResource GetStatResourse(Stat stat) {
        // StatResource result = null;
		// foreach(StatResource element in stats) {
		// 	if(element.StatType == stat ) {
		// 		result = element;
		// 	}
		// }
		// return result;

		return stats.Where((element) => element.StatType == stat).FirstOrDefault();
    }

    public void Flip(){
		bool isNotMovingHorizontally = Velocity.X == 0;

		if (!isNotMovingHorizontally) {
			bool isMovingLeft = Velocity.X < 0;
			
			SpriteNode.FlipH = isMovingLeft;
		}
		
	}

	public void ToggleHitBox(bool flag) {
		HitBoxShapeNode.Disabled = flag;
	}


}