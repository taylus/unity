using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float InitialSpeed;
    private Rigidbody2D body;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (body == null) throw new MissingComponentException("Expected: Rigidbody2D");
        Reset();
    }

    public void OnBecameInvisible()
    {
        Reset();
    }

    private void Reset()
    {
        body.position = Vector2.zero;
        body.velocity = GetRandomVelocity();
    }

    private Vector2 GetRandomVelocity()
    {
        float angle = Common.GetRandomAngle();
        return Common.GetPointOnCircle(angle, InitialSpeed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            //TODO: play sound
            body.velocity *= 1.1f;
        }
    }
}
