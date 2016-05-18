using UnityEngine;
using System.Collections;

public class Roly : MonoBehaviour
{
    public GameObject Mouth;

    //detect when food enters Roly's collision trigger
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Food food = collider.gameObject.GetComponent<Food>();
        if(food != null)
        {
            Eat(food);
            if (food.IsTasty)
            {
                Smile();
            }
            else
            { 
                Frown();
            }
        }
    }

    //detect when food stays inside Roly's collision trigger
    //just in case OnTriggerEnter doesn't fire, which seems to happen sometimes when there's a lot of food
    public void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerEnter2D(collider);
    }

    private void Eat(Food food)
    {
        StartCoroutine(Grow(food.Calories * 0.00005f /* metabolic magic number */));
        Destroy(food.gameObject);
    }

    private void Smile()
    {
        Mouth.transform.eulerAngles = Vector3.zero;
    }

    private void Frown()
    {
        Mouth.transform.eulerAngles = Vector3.right * 180f;
    }

    //increase Roly's scale by the given amount in the given number of steps over the given duration
    private IEnumerator Grow(float amount, int steps = 8, float duration = 0.15f)
    {
        Vector3 scale = new Vector3(amount / steps, amount / steps);
        for (int i = 0; i < steps; i++)
        {
            transform.localScale += scale;
            if(transform.localScale.x < 0.2f)
            {
                transform.localScale = new Vector3(0.2f, 0.2f);
            }
            yield return new WaitForSeconds(duration / steps);
        }
    }
}
