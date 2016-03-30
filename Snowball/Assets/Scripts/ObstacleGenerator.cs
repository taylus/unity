using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Periodically spawns prefab game objects and attaches them to itself.
/// </summary>
public class ObstacleGenerator : MonoBehaviour
{
    [Tooltip("How long to wait (in seconds) between spawning obstacles.")]
    public float SpawnInterval = 5.0f;

    [Tooltip("How many world units spawned obstacles are moved up each frame.")]
    public float MovementSpeed = 0.1f;

    [Tooltip("Prefab obstacles to spawn.")]
    public List<GameObject> ObstaclesToSpawn;

    /// <summary>
    /// Called when this script is initialized.
    /// </summary>
    public void Awake()
    {
        StartCoroutine("Spawn");
    }

    /// <summary>
    /// Spawns a random prefab from <see cref="ObstaclesToSpawn"/> every <see cref="SpawnInterval"/> seconds.
    /// </summary>
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnInterval);
            if (ObstaclesToSpawn == null || ObstaclesToSpawn.Count == 0) yield break;
            GameObject whatToSpawn = ObstaclesToSpawn[Random.Range(0, ObstaclesToSpawn.Count)];
            var spawned = Instantiate(whatToSpawn, GetRandomSpawnPoint(whatToSpawn), Quaternion.identity) as GameObject;
            spawned.transform.parent = transform;
        }
    }

    /// <summary>
    /// Returns a random point for the given object to spawn at the bottom of the screen.
    /// </summary>
    private Vector2 GetRandomSpawnPoint(GameObject obj)
    {
        float camSize = Camera.main.orthographicSize;

        //spawn this far below the bottom of the screen so the object doesn't pop in
        float halfHeight = (obj.GetComponent<Renderer>().bounds.size.y / 2);

        return new Vector2(Random.Range(-camSize, camSize), -(camSize + halfHeight));
    }

    /// <summary>
    /// Called at a fixed rate (the game's framerate).
    /// Moves spawned obstacles, destroying them if they go offscreen.
    /// </summary>
    public void FixedUpdate()
    {
        foreach (Transform child in transform)
        {
            child.position += new Vector3(0, MovementSpeed);
            var renderer = child.GetComponent<SpriteRenderer>();
            if(renderer != null && !renderer.isVisible)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
