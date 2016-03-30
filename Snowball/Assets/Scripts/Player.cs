using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Tooltip("Max movement speed in world units/sec.")]
    public float MaxSpeed = 1.0f;

    [Tooltip("The y-coordinate at which the player will stop moving down the screen.")]
    public float MinVerticalPosition = 3;

    /// <summary>
    /// The player's physics body.
    /// </summary>
    private Rigidbody2D body;

    /// <summary>
    /// Called when this script is initialized.
    /// </summary>
    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (body == null) throw new MissingComponentException("Expected attached Rigidbody2D");

        StartCoroutine("Grow");
    }

    /// <summary>
    /// Called at a fixed rate (the game's framerate). Handles input.
    /// </summary>
    public void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal") * 8;
        float downwardForce = body.position.y <= MinVerticalPosition ? 0 : -1;
        body.AddForce(new Vector2(input, downwardForce));
        body.MoveRotation(body.rotation - input);
        body.velocity = Vector2.ClampMagnitude(body.velocity, MaxSpeed);
    }

    /// <summary>
    /// Makes the player snowball grow larger and heavier at a fixed rate.
    /// </summary>
    private IEnumerator Grow()
    {
        while (transform.localScale.x <= 1.0f)
        {
            transform.localScale += new Vector3(0.001f, 0.001f, 0);
            body.mass += 0.005f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            //use the horizontal distance between the two objects' centers to determine the severity of the collision
            //only get a little smaller if it's a glancing blow,
            //but get a lot smaller/probably die if it's a head-on collision
        }
    }
}
