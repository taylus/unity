using UnityEngine;

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