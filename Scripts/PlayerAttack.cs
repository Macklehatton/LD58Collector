using Godot;
using System;
using Godot.Collections;
using System.Diagnostics;

public partial class PlayerAttack : Node2D
{
    [Export] private RandomSword weaponObject;
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
                hit = false;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!attacking)
        {
            return;
        }

        Swing((float)delta);

        if (hit)
        {
            return;
        }

        CheckHit();
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
                    hit = true;
                }
            }
        }
    }

    private void Swing(float delta)
    {
        swingProgress += delta;

        float swingFactor = swingProgress / swingTime;

        weaponObject.Rotation = Mathf.Lerp(-swingAngle, swingAngle, swingFactor);

        trail.UpdateTrail(swingProgress, weaponObject.SwordTip);

        if (swingProgress >= swingTime)
        {
            StopSwing();
        }
    }

    private void StopSwing()
    {
        weaponObject.Rotation = 0.0f;
        swingProgress = 0.0f;
        attacking = false;
        trail.StopTrail();
    }

    public void ChangeWeapon(RandomSword randomSword)
    {
        weaponObject = randomSword;
        weaponArea = randomSword.CollisionArea;
    }
}
