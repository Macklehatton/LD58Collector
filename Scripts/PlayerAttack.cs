using Godot;
using System;
using Godot.Collections;
using System.Diagnostics;

public partial class PlayerAttack : Node2D
{
    [Export] private Node2D weaponObject;
    [Export] private Area2D weaponArea;

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
    }

    public override void _PhysicsProcess(double delta)
    {
        if (attacking)
        {
            Swing((float)delta);
            CheckHit();
        }
    }

    public void CheckHit()
    {
        if (weaponArea.HasOverlappingAreas())
        {
            Array<Area2D> bodies = weaponArea.GetOverlappingAreas();
            foreach (Area2D body in bodies)
            {
                if (body.IsInGroup(targetGroup))
                {
                    ((Enemy)body).TakeDamage();
                }
            }
        }
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
