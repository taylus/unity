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

    private LineRenderer lineRenderer;

    public void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.025f, 0.025f);
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
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (2f * Mathf.PI) / Points;
        }
    }
}
