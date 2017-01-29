using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A color that keeps track of its own name.
/// </summary>
public struct NamedColor
{
    public string Name { get; private set; }
    public Color Color { get; private set; }

    public NamedColor(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public NamedColor(string name, float r, float g, float b)
    {
        Name = name;
        Color = new Color(r, g, b);
    }
}

public static class NamedColors
{
    //TODO: have the current level maintain a subset of these, harder levels -> more colors
    public static List<NamedColor> Colors = new List<NamedColor>
    {
        new NamedColor("Red", new Color(0.75f, 0, 0)),
        //new NamedColor("Green", new Color(0, 0.75f, 0)),
        new NamedColor("Blue", new Color(0, 0, 0.75f)),
        //new NamedColor("Yellow", new Color(0.9f, 0.9f, 0)),
        //new NamedColor("Purple", new Color(0.5f, 0, 0.5f)),
        //new NamedColor("Orange", new Color(0.9f, 0.4f, 0)),
        //new NamedColor("Pink", new Color(0.9f, 0.4f, 0.9f)),
        //new NamedColor("Gray", new Color(0.5f, 0.5f, 0.5f)),
        //new NamedColor("Black", new Color(0, 0, 0)),
        //new NamedColor("White", new Color(1, 1, 1)),
    };

    public static NamedColor GetRandom()
    {
        return Colors[Random.Range(0, Colors.Count)];
    }
}