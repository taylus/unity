using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //TODO: make this manage the whole game?
    //pick a current color the player should click
    //every few seconds, switch it
    //rack up combos if the player keeps getting the color right

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

    public void Start()
    {
        foreach (Transform child in transform)
        {
            Ball ball = child.GetComponent<Ball>();
            ball.Color = Colors[Random.Range(0, Colors.Count)];
        }
    }
}
