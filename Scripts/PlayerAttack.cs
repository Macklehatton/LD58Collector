using Godot;
using System;

public partial class PlayerAttack : Node2D
{
    [Export] private PlayerWeapon weapon;
    [Export] private float swingTime;
    [Export] private float swingAngle;

    private bool swinging;
    private bool hit;

    private float swingProgress;


    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("PrimaryAttack"))
        {
            Attack();
        }

        if (swinging)
        {
            Swing();
        }
    }

    public void Attack()
    {
        // Swing animation

        // Detect (one?) hit during swing
    }

    private void Swing()
    {

    }
}
