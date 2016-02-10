using UnityEngine;
using System.Collections;

/// <summary>
/// The player controlled-paddle. Bound to a circle that it moves around, following the mouse.
/// </summary>
public class Paddle : MonoBehaviour
{
    [Tooltip("The radius of the invisible circle this paddle is bound to. Measured in world units, so if your camera's orthographic size is 10, make this 10 to fill the screen.")]
    public float BoundsRadius = 5;

    internal float CurrentAngle { get; private set; }

    public void Update()
    {
        if (Input.GetKey("escape")) Application.Quit();

        Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CurrentAngle = Common.GetAngleOnUnitCircle(mouseWorldPoint);
        transform.position = Common.GetPointOnCircle(CurrentAngle, BoundsRadius);
        transform.eulerAngles = new Vector3(0, 0, CurrentAngle * Mathf.Rad2Deg);
    }
}