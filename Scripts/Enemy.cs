using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] private Node2D player;
    [Export] private float aggroRange;
    [Export] private float speed;
    [Export] private float waryDistance;
    [Export] private Node2D weapon;

    private EnemyState state;
    private Node2D orbitPivot;
    private Node2D orbitObject;

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
        if (state == EnemyState.wary)
        {
            Wary((float)delta);
        }

        LookAt(player.GlobalPosition);
        Rotate(Mathf.Pi / 2.0f);
    }

    private void Wary(float delta)
    {
        float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
        Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();

        GlobalPosition = GlobalPosition.MoveToward(player.GlobalPosition - (direction * waryDistance), speed * delta);

        //GlobalPosition = GlobalPosition.MoveToward(orbitObject.GlobalPosition, speed * delta);
    }

    public void TakeDamage()
    {
        weapon.Reparent(GetTree().Root);
        QueueFree();
    }
}
