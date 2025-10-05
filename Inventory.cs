using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
    [Export] private GridContainer gridContainer;
    [Export] private Control itemTile;

    private List<RandomSword> inventory;

    public void AddItem()
    {
        Control instance = (Control)itemTile.Duplicate();
        gridContainer.AddChild(instance);
    }
}
