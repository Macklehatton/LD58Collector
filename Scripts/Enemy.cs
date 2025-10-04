using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    [Export] private Node2D player;
    [Export] private float aggroRange;
    [Export] private float speed;
    [Export] private float waryDistance;

    private EnemyState state;

    public override void _Ready()
    {
        //state = EnemyState.idle;
        state = EnemyState.wary;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (state == EnemyState.wary)
        {
            Wary();
        }

        MoveAndSlide();
    }

    private void Wary()
    {
        float distance = GlobalPosition.DistanceTo(player.GlobalPosition);

        if (distance > waryDistance)
        {

        }
    }

    public void TakeDamage()
    {
        QueueFree();
    }
}
