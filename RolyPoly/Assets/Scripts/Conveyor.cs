using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector2 Direction;

    public void OnCollisionStay2D(Collision2D collision)
    {
        Food food = collision.gameObject.GetComponent<Food>();
        food.MoveRelative(Direction);
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
