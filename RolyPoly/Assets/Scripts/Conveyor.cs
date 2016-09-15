using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector2 Direction;

    public void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();  
        body.MovePosition(new Vector2(body.position.x + Direction.x, body.position.y + Direction.y));
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Direction = -Direction;
        }
    }

    public void Update()
    {
        SpinWheels();
    }

    public void SpinWheels()
    {
        foreach(Transform child in transform)
        {
            if(child.CompareTag("Wheel"))
            {
                child.transform.Rotate(Vector3.back, Direction.x * 100f);
            }
        }
    }
}
