using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] private Node2D player;
    [Export] private float aggroRange;
    [Export] private float speed;
    [Export] private float waryDistance;
    [Export] private RandomSword weapon;
    [Export] private float strafeDistance;
    [Export] private float strafeRate;
    [Export] private float rotationSpeed;

    private EnemyState state;
    private Node2D orbitPivot;
    private Node2D orbitObject;

    private float currentStrafeOffset;
    private float time;

    public override void _Ready()
    {
        //state = EnemyState.idle;
        state = EnemyState.wary;

        orbitPivot = new Node2D();
        orbitObject = new Node2D();

        orbitPivot.AddChild(orbitObject);

        player.AddChild(orbitPivot);

        orbitPivot.Rotate(Mathf.Pi / 4.0f);

        orbitPivot.Position = Vector2.Zero;
        orbitObject.Position = new Vector2(0.0f, -waryDistance);
    }

    public override void _PhysicsProcess(double delta)
    {
        time += (float)delta;

        currentStrafeOffset = Mathf.Sin(time * strafeRate);
        currentStrafeOffset *= strafeDistance;

        if (state == EnemyState.wary)
        {
            Wary((float)delta);
        }
    }

    private void Wary(float delta)
    {
        Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();

        Vector2 desiredPosition = player.GlobalPosition - (direction * waryDistance);
        Vector2 tangent = direction.Normalized().Rotated(Mathf.Pi / 2.0f);
        desiredPosition += tangent * currentStrafeOffset;

        GlobalPosition = GlobalPosition.MoveToward(desiredPosition, speed * delta);

        float desiredRotation = GlobalTransform.X.AngleTo(direction);
        GlobalRotation = Mathf.MoveToward(GlobalRotation, desiredRotation, rotationSpeed);

        //Rotate(Mathf.Pi / 2.0f);
    }

    public void TakeDamage()
    {
        RandomSword dropped = RandomSword.GetDuplicate(weapon);
        dropped.Position = GlobalPosition;
        dropped.Rotation = GlobalRotation;
        GetTree().Root.AddChild(dropped);
        dropped.SetDropped();

        Respawn();
    }

    public void Respawn()
    {
        weapon.Randomize();
    }
}
