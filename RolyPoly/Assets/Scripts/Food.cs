using UnityEngine;

public class Food : MonoBehaviour
{
    [Tooltip("What is this food called?")]
    public string Name;

    [Tooltip("How fat will Roly get from eating this?")]
    public float Calories;

    public bool IsTasty { get { return Calories > 0; } }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void MoveRelative(Vector2 delta)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.MovePosition(new Vector2(body.position.x + delta.x, body.position.y + delta.y));
    }

    public void SetCollision(bool enabled)
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = enabled;
        collider.isTrigger = enabled;
    }

    public void StopMovement()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
    }
}
