using Godot;
using System;

public partial class Trail : Line2D
{
    [Export] private int pointCount;

    //private Vector2 trailPosition;
    private int pointsAdded;

    public override void _Ready()
    {
        //trailPosition = swordTip.GlobalPosition;
    }

    public void StopTrail()
    {
        Reset();
    }

    public void UpdateTrail(float factor, Vector2 swordTip)
    {
        int currentPoint = (int)(pointCount * factor);

        if (pointsAdded >= currentPoint)
        {
            return;
        }

        //trailPosition = trailPosition.MoveToward(swordTip.GlobalPosition, trailSpeed * (float)delta);

        AddPoint(swordTip);
    }

    private void Reset()
    {
        ClearPoints();
        pointsAdded = 0;
    }
}
