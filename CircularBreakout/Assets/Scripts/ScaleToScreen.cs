using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this script to an object and it will scale to the fit the screen.
/// </summary>
public class ScaleToScreen : MonoBehaviour
{
    public void Awake()
    {
        float height = Camera.main.orthographicSize * 2;
        float aspectRatio = (float)Screen.width / Screen.height;
        float width = height * aspectRatio;
        transform.localScale = new Vector3(width, height, 1);
    }
}
