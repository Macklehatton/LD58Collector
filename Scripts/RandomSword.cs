using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

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
    [Export] private Area2D collisionArea;

    [ExportToolButton("Randomize")]
    public Callable RandomizeButton => Callable.From(Randomize);

    public bool Dropped { get; set; }
    public Sprite2D Sprite { get => sprite; set => sprite = value; }

    public float Width;
    public float Length;

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

    public void Randomize()
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        Width = rng.RandfRange(minWidth, maxWidth);
        Length = rng.RandfRange(minLength, maxLength);

        sprite.Scale = new Vector2(Width, Length);
        OffsetBladeSprite(Width, Length);
    }

    public void OffsetBladeSprite(float width, float length)
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

    public void SetDropped()
    {
        collisionArea.AddToGroup("Pickup");
    }

    public static RandomSword GetDuplicate(RandomSword original)
    {
        RandomSword copy = (RandomSword)original.Duplicate();

        //scopy.Width = Width;
        //copy.Length = Length;
        //copy.sprite.Scale = new Vector2(Width, Length);
        //copy.OffsetBladeSprite(Width, Length);

        return copy;
    }
}
