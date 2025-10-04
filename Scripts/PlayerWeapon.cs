using Godot;
using System;

public partial class PlayerWeapon : Node2D
{
    public override void _Process(double delta)
    {
        var camera = GetViewport().GetCamera2D();
        Vector2 mousePosition = camera.GetGlobalMousePosition();
        //Vector2 direction = GlobalPosition - mousePosition;

        LookAt(mousePosition);
        Rotate(Mathf.Pi / 2.0f);
    }
}
