using UnityEngine;
using System.Collections;

/// <summary>
/// Loads game levels!
/// </summary>
public class LevelLoader : MonoBehaviour
{
    public Transform BlockPrefab;

    public void Awake()
    {
        Load();
    }

    private void Load()
    {
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                Instantiate(BlockPrefab, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    //TODO: load levels from files or something
}
