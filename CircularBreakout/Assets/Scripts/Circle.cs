using UnityEngine;
using System.Collections;

/// <summary>
/// Approximates a circle by drawing a number of points with a LineRenderer.
/// </summary>
public class Circle : MonoBehaviour
{
    [Tooltip("The radius of the circle, measured in world units.")]
    public float Radius = 5;

    [Tooltip("The number of vertices in the circle.")]
    public int Points = 100;

    [Tooltip("The color of the circle.")]
    public Color Color;

    private LineRenderer lineRenderer;

    public void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color;
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(Points + 1);    //+1 to close the shape
        Draw();
    }

    private void Draw()
    {
        float angle = 0f;
        for(int i = 0; i <= Points; i++)
        {
            float x = Radius * Mathf.Cos(angle) + transform.position.x;
            float y = Radius * Mathf.Sin(angle) + transform.position.y;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0.01f)); //Z is slightly behind the paddle so it draws in front
            angle += (2f * Mathf.PI) / Points;
        }
    }

    private void OnDestroy()
    {
        Destroy(lineRenderer.material);
    }
}
