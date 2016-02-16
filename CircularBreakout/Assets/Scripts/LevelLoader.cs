using UnityEngine;
using System.Collections;

/// <summary>
/// Loads game levels!
/// </summary>
public class LevelLoader : MonoBehaviour
{
    public GameObject BlockPrefab;
    public int BlockCount { get; private set; }

    public void Awake()
    {
        Load();
    }

    public void Load()
    {
        BlockCount = 0;
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                Instantiate(BlockPrefab, new Vector2(x, y), Quaternion.identity);
                BlockCount++;
            }
        }
    }

    public void DestroyBlock(GameObject block)
    {
        Destroy(block);
        BlockCount--;
    }

    public bool GameOver()
    {
        return BlockCount <= 0;
    }

    //TODO: load levels from files or something
    //TODO: pool blocks instead of Instantiate/Destroy?
}
