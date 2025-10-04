using Godot;
using System;

[Tool]
public partial class RandomSword : Node2D
{
    [Export] private float minLength;
    [Export] private float maxLength;

    [Export] private float minWidth;
    [Export] private float maxWidth;

    [Export] Sprite2D sprite;

    [ExportToolButton("Randomize")]
    public Callable RandomizeButton => Callable.From(Randomize);

    [ExportToolButton("Min")]
    public Callable MinButton => Callable.From(Min);
    [ExportToolButton("Max")]
    public Callable MaxButton => Callable.From(Max);


    public override void _Ready()
    {
        Randomize();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Randomize"))
        {
            Randomize();
        }
    }

    private void Randomize()
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        float length = rng.RandfRange(minLength, maxLength);
        float width = rng.RandfRange(minWidth, maxWidth);

        sprite.Scale = new Vector2(width, length);
        OffsetBlade();
    }

    private void Min()
    {
        sprite.Scale = new Vector2(minWidth, minLength);
        OffsetBlade();
    }

    private void Max()
    {
        sprite.Scale = new Vector2(maxWidth, maxLength);
        OffsetBlade();
    }

    private void OffsetBlade()
    {
        float pixelHeight = sprite.Texture.GetHeight();
        sprite.Offset = new Vector2(0.0f, -pixelHeight / 2.0f);
    }
}
