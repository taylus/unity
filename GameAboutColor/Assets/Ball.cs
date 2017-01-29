using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Ball : MonoBehaviour
{
    private new Renderer renderer;
    private NamedColor color;
    public NamedColor Color
    {
        get { return color; }
        set
        {
            color = value;
            renderer.material.color = value.Color;
        }
    }

    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void OnMouseDown()
    {
        Debug.LogFormat("Clicked on a {0} ball", color.Name);
    }
}
