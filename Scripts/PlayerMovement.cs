using Godot;
using System;
using System.Diagnostics;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] private float speed;

    public override void _PhysicsProcess(double delta)
    {
        UpdateRotation();

        float yInput = Input.GetAxis("Down", "Up");
        float xInput = Input.GetAxis("Left", "Right");

        Vector2 movement = new Vector2(xInput, -yInput);

        Velocity = movement.Normalized() * speed;

        MoveAndSlide();
    }

    public void UpdateRotation()
    {
        var camera = GetViewport().GetCamera2D();
        Vector2 mousePosition = camera.GetGlobalMousePosition();

        LookAt(mousePosition);
        Rotate(Mathf.Pi / 2.0f);
    }
}
