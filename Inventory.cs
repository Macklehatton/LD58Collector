using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Inventory : Node
{
    [Export] private GridContainer gridContainer;
    [Export] private PackedScene itemTile;
    [Export] private Node2D weaponContainer;
    [Export] private PlayerAttack playerAttack;
    [Export] private Sprite2D selection;

    private List<RandomSword> inventory;

    private int selectedIndex;
    private RandomSword equippedWeapon;

    public override void _Ready()
    {
        inventory = new List<RandomSword>();
        equippedWeapon = (RandomSword)weaponContainer.GetChild(0);
        AddItem(equippedWeapon);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("NextWeapon"))
        {
            SwitchNext();
        }
    }

    public void SwitchNext()
    {
        if (inventory.Count == 1)
        {
            return;
        }

        selectedIndex += 1;
        if (selectedIndex > inventory.Count - 1)
        {
            selectedIndex = 0;
        }

        Control child = (Control)gridContainer.GetChild(selectedIndex);
        selection.GlobalPosition = child.GlobalPosition;


        Unequip(equippedWeapon);
        Equip(selectedIndex);
    }

    public void AddItem(RandomSword item)
    {
        Control instance = (Control)itemTile.Instantiate();
        gridContainer.AddChild(instance);

        inventory.Add(item);
    }

    public void Equip(int selectedIndex)
    {
        RandomSword equipping = inventory[selectedIndex];
        equipping.Reparent(weaponContainer);
        equipping.Visible = true;
        equipping.CollisionShape.Disabled = false;
        equipping.Position = Vector2.Zero;
        equipping.Rotation = 0.0f;

        equippedWeapon = equipping;
        playerAttack.ChangeWeapon(equippedWeapon);
    }

    public void Unequip(RandomSword sword)
    {
        sword.Visible = false;
        sword.CollisionShape.Disabled = true;
    }
}
