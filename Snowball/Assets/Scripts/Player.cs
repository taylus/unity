using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Tooltip("Max movement speed in world units/sec.")]
    public float MaxSpeed = 1.0f;

    private Rigidbody2D body;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (body == null) throw new MissingComponentException("Expected attached Rigidbody2D");

        StartCoroutine("Grow");
    }

    public void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal") * 8;
        body.AddForce(new Vector2(input, 0));
        body.velocity = Vector2.ClampMagnitude(body.velocity, MaxSpeed);
    }

    private IEnumerator Grow()
    {
        while (transform.localScale.x <= 1.0f)
        {
            transform.localScale += new Vector3(0.001f, 0.001f, 0);
            body.mass += 0.005f;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
