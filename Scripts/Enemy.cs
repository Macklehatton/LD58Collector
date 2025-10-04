using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] private Node2D player;
    [Export] private float aggroRange;
    [Export] private float speed;
    [Export] private float waryDistance;

    private EnemyState state;
    private Node2D orbitObject;

    public override void _Ready()
    {
        //state = EnemyState.idle;
        state = EnemyState.wary;

        orbitObject = new Node2D();
        player.AddChild(orbitObject);
        orbitObject.Position = Vector2.Zero;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (state == EnemyState.wary)
        {
            Wary();
        }

        //MoveAndSlide();
    }

    private void Wary()
    {
        float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
        Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();

        // if (distance > waryDistance)
        // {
        //     Velocity = direction * speed;
        // }
        // else
        // {
        //     Vector2 desiredPostion =
        //     Velocity = Vector2.Zero;
        // }
    }

    public void TakeDamage()
    {
        QueueFree();
    }
}
