using UnityEngine;

/// <summary>
/// Creates <see cref="Fuel"/> prefabs randomly within a space. 
/// </summary>
public class FuelManager : MonoBehaviour
{
    public Fuel FuelPrefab;
    public Rect SpawnArea;
    public float SpawnDistance;

    private void OnDrawGizmosSelected()
    {
        //draw the spawn area in the editor window
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(transform.position + (Vector3)SpawnArea.position, SpawnArea.size);
    }

    private void Awake()
    {
        SpawnFuelCollectables();
    }

    private void SpawnFuelCollectables()
    {
        for (float y = SpawnDistance; y <= SpawnArea.height; y += SpawnDistance)
        {
            var spawnedFuel = Instantiate(FuelPrefab, transform);
            spawnedFuel.name = "Fuel " + y;
            spawnedFuel.transform.position = new Vector2(Random.Range(-SpawnArea.width / 2, SpawnArea.width / 2), y);
        }
    }
}
