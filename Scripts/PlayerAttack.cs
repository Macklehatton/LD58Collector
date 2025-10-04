using Godot;
using System;
using System.Diagnostics;

public partial class PlayerAttack : Node2D
{
    [Export] private Node2D weaponObject;
    [Export] private float swingTime;
    [Export] private float swingAngle;

    private bool attacking;
    private bool hit;

    private float swingProgress;

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("PrimaryAttack"))
        {
            if (!attacking)
            {
                attacking = true;
            }
        }

        if (attacking)
        {
            Swing((float)delta);
            CheckHit();
        }
    }

    public void CheckHit()
    {
        // Detect (one?) hit during swing
    }

    private void Swing(float delta)
    {
        swingProgress += delta;

        weaponObject.Rotation = Mathf.Pi / 2.0f;

        if (swingProgress >= swingTime)
        {
            weaponObject.Rotation = 0.0f;
            swingProgress = 0.0f;
            attacking = false;
        }
    }
}
