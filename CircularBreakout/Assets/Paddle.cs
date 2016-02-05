using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
    [Tooltip("The radius of the invisible circle this paddle is bound to. Measured in world units, so if your camera's orthographic size is 10, make this 10 to fill the screen.")]
    public float BoundsRadius;

    public void Update()
    {
        if (Input.GetKey("escape")) Application.Quit();

        Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseAngle = Common.GetAngleOnUnitCircle(mouseWorldPoint);
        transform.position = Common.GetPointOnCircle(mouseAngle, BoundsRadius);
        transform.eulerAngles = new Vector3(0, 0, mouseAngle * Mathf.Rad2Deg);
    }
}