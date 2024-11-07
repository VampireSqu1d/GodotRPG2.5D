using System;

public class GameConstants {

    // player animations constants
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_MOVE = "Move";
    public const string ANIM_DASH = "Dash";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DEATH = "Death";
    public const string ANIM_EXPAND = "Expand";
    public const string ANIM_EXPLOSION = "Explosion";
    public const string ANIM_STUN = "Stun";
    
    //move input actions
    public const string INPUT_MOVE_LEFT = "move_left";
    public const string INPUT_MOVE_RIGHT = "move_right";
    public const string INPUT_MOVE_FORWARD = "move_forward";
    public const string INPUT_MOVE_BACKWARD = "move_backward";
    public const string INPUT_DASH = "dash";
    public const string INPUT_ATTACK = "attack";
    public const string INPUT_PAUSE = "pause";
    public const string INPUT_INTERACT = "interact";
    
    // Notification numbers
    public const int NOTIFICATION_ENTER_STATE = 5001;
    public const int NOTIFICATION_EXIT_STATE = 5002;
}