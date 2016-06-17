using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector2 Direction;

    public void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(Direction, ForceMode2D.Force);
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Direction = -Direction;
        }
    }
}
