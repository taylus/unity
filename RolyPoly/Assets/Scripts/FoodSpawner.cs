using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    [Tooltip("Food prefabs to spawn.")]
    public List<Food> Food;

    [Tooltip("How long (in seconds) to wait in between spanwning food.")]
    public float SpawnDelay;

    [Tooltip("How long (in seconds) to wait before spawning the first food item.")]
    public float InitialDelay;

    [Tooltip("Point at which food is spawned (world coordinates).")]
    public Vector2 SpawnPoint;

    public void Start()
    {
        StartCoroutine(SpawnRandom());
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(SpawnPoint, new Vector3(0.2f, 0.2f, 0.2f));
    }

    public IEnumerator SpawnRandom()
    {
        yield return new WaitForSeconds(InitialDelay);
        while (true)
        {
            Food randomFood = Food[Random.Range(0, Food.Count)];
            Spawn(randomFood, SpawnPoint);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    private void Spawn(Food food, Vector3 position)
    {
        Food spawned = Instantiate(food, position, Quaternion.identity) as Food;
        spawned.transform.parent = transform;
    }
}
