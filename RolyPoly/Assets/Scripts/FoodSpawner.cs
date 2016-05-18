using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    public List<Food> Food;

    public void Start()
    {
        //StartCoroutine(SpawnRandom());
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Food == null || Food.Count < 1) return;
            Spawn(Food[0], ToWorld(Input.mousePosition));
        }

        if(Input.GetMouseButtonDown(1))
        {
            if (Food == null || Food.Count < 2) return;
            Spawn(Food[1], ToWorld(Input.mousePosition));
        }
    }

    private void Spawn(Food food, Vector3 position)
    {
        Food spawned = Instantiate(food, position, Quaternion.identity) as Food;
        spawned.transform.parent = transform;
    }

    public IEnumerator SpawnRandom()
    {
        while(true)
        {
            Food randomFood = Food[Random.Range(0, Food.Count)];
            Vector3 randomPosition = new Vector3(Random.Range(0, Screen.width), Screen.height);
            Spawn(randomFood, ToWorld(randomPosition));
            yield return new WaitForSeconds(5);
        }
    }

    private Vector2 ToWorld(Vector2 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }
}
