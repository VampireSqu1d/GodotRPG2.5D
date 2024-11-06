using Godot;
using System;

public partial class AbilitiesHitBox : Area3D, IHitBox {

    public float GetDamage() => GetOwner<Ability>().Damage;
}
