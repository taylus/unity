using UnityEngine;
using System.Collections;

public static class Common
{
    public static float GetAngleOnUnitCircle(Vector2 point)
    {
        return Mathf.Atan2(point.y, point.x);
    }

    public static Vector2 GetPointOnCircle(float angle, float radius)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    public static float GetRandomAngle()
    {
        return Random.Range(-Mathf.PI, Mathf.PI);
    }
}
