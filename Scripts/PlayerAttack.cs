using Godot;
using System;
using System.Diagnostics;

public partial class PlayerAttack : Node2D
{
    [Export] private Node2D weaponObject;
    [Export] private Trail trail;
    [Export] private float swingTime;
    [Export] private float swingAngle;
    [Export] private string targetGroup;

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

        float swingFactor = swingProgress / swingTime;

        weaponObject.Rotation = Mathf.Lerp(-swingAngle, swingAngle, swingFactor);

        trail.UpdateTrail(swingProgress);

        if (swingProgress >= swingTime)
        {
            weaponObject.Rotation = 0.0f;
            swingProgress = 0.0f;
            attacking = false;
            trail.StopTrail();
        }
    }
}
