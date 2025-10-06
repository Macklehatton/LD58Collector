using Godot;
using System;
using Godot.Collections;
using System.Diagnostics;

public partial class PickupItem : Node2D
{
    [Export] private Area2D pickupArea;
    [Export] private string pickupGroup;
    [Export] private Inventory inventory;

    public override void _Process(double delta)
    {
        if (pickupArea.HasOverlappingAreas())
        {
            Array<Area2D> areas = pickupArea.GetOverlappingAreas();
            foreach (Area2D area in areas)
            {
                if (!area.IsInGroup(pickupGroup))
                {
                    continue;
                }

                RandomSword item = (RandomSword)area.GetParent();

                if (!item.Dropped)
                {
                    continue;
                }

                inventory.AddItem(item);

                item.Visible = false;
                item.Dropped = false;

                return;
            }
        }
    }
}
