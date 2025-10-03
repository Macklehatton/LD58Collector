using Godot;
using System;
using System.Diagnostics;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] private float speed;

    public override void _Process(double delta)
    {
        float yInput = Input.GetAxis("Down", "Up");
        float xInput = Input.GetAxis("Left", "Right");

        Vector2 movement = new Vector2(xInput, -yInput);

        Velocity = movement.Normalized() * speed;

        MoveAndSlide();
    }
}
