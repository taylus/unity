using UnityEngine;
using System.Collections;

public class Roly : MonoBehaviour
{
    public GameObject Mouth;

    public void Start()
    {
        StartCoroutine(Breathe());
    }

    //detect when food enters Roly's collision trigger
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Food food = collider.gameObject.GetComponent<Food>();
        if (food != null)
        {
            Eat(food);
        }
    }

    //detect when food stays inside Roly's collision trigger
    //just in case OnTriggerEnter doesn't fire, which seems to happen sometimes when there's a lot of food
    public void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerEnter2D(collider);
    }

    //make Roly go :)
    public IEnumerator Smile(float afterSeconds = 0)
    {
        yield return new WaitForSeconds(afterSeconds);
        Mouth.transform.eulerAngles = Vector3.zero;
    }

    //make Roly go :(
    public IEnumerator Frown(float afterSeconds = 0)
    {
        yield return new WaitForSeconds(afterSeconds);
        Mouth.transform.eulerAngles = Vector3.right * 180f;
    }

    //make Roly slowly increase and decrease in size, to look like breathing
    public IEnumerator Breathe(float breathSize = 0.0002f)
    {
        while(true)
        {
            float breathAmount = Mathf.Sin(Time.fixedTime) * breathSize;
            transform.localScale += new Vector3(breathAmount, breathAmount);
            yield return new WaitForEndOfFrame();
        }
    }

    //eat the given food
    private void Eat(Food food)
    {
        food.SetCollision(false);
        food.StopMovement();
        const float eatDuration = 0.4f;
        StartCoroutine(this.Scale(transform.localScale.x + (food.Calories * 0.00002f) /* metabolic magic number */, eatDuration, eatDuration));
        StartCoroutine(food.MoveTo(Mouth.transform.position, eatDuration));
        StartCoroutine(food.Scale(0.5f, eatDuration));
        if (food.IsTasty)
        {
            StartCoroutine(Smile(eatDuration));
        }
        else
        {
            StartCoroutine(Frown(eatDuration));
        }
        Destroy(food.gameObject, eatDuration);
    }
}
