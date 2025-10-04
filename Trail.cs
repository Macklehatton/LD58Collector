using Godot;
using System;

public partial class Trail : Line2D
{
    [Export] private Node2D swordTip;
    [Export] private int pointCount;

    [Export] private double rate;

    private double timeSinceUpdate;

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
        timeSinceUpdate += delta;
        if (timeSinceUpdate < rate)
        {
            return;
        }

        timeSinceUpdate = 0.0f;

        AddPoint(swordTip.GlobalPosition);

        if (Points.Length > pointCount)
        {
            RemovePoint(0);
        }
    }
}
