using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
    [Export] private GridContainer gridContainer;
    [Export] private PackedScene itemTile;

    private List<RandomSword> inventory;

    private int selectedIndex;

    public void AddItem()
    {
        Control instance = (Control)itemTile.Instantiate();
        gridContainer.AddChild(instance);
    }
}
