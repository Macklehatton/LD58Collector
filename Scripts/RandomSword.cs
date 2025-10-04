using Godot;
using System;

[Tool]
public partial class RandomSword : Node2D
{
    [Export] private float minLength;
    [Export] private float maxLength;

    [Export] private float minWidth;
    [Export] private float maxWidth;

    [Export] private Sprite2D sprite;
    [Export] private Node2D swordTip;
    [Export] private CollisionShape2D collisionShape;

    [ExportToolButton("Randomize")]
    public Callable RandomizeButton => Callable.From(Randomize);

    public override void _Ready()
    {
        Randomize();
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            return;
        }
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
        OffsetBladeSprite(width, length);
    }

    private void OffsetBladeSprite(float width, float length)
    {
        float pixelHeight = sprite.Texture.GetHeight();
        float pixelWidth = sprite.Texture.GetWidth();

        sprite.Offset = new Vector2(0.0f, -pixelHeight / 2.0f);
        swordTip.Position = new Vector2(0.0f, -pixelHeight * length);

        collisionShape.Shape = new RectangleShape2D()
        {
            Size = new Vector2(pixelWidth * width, pixelHeight * length)
        };

        collisionShape.Position = new Vector2(0.0f, -(pixelHeight * length) / 2.0f);
    }
}
