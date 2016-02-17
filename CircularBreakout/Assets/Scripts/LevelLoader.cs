using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Loads game levels from textures.
/// </summary>
public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// The game object that is used as a breakout block.
    /// </summary>
    public GameObject BlockPrefab;

    /// <summary>
    /// And array of textures used for levels.
    /// Each pixel is loaded as a breakout block.
    /// </summary>
    public Texture2D[] Levels;

    /// <summary>
    /// The number of block prefabs current in the level.
    /// When zero, the level is clear.
    /// </summary>
    public int BlockCount { get; private set; }

    public void Awake()
    {
        LoadRandom();
    }

    /// <summary>
    /// Loads a level from the Levels array at random.
    /// </summary>
    public void LoadRandom()
    {
        int index = UnityEngine.Random.Range(0, Levels.Length);
        Load(Levels[index]);
    }

    /// <summary>
    /// Scans through the pixels in the given texture and loads breakout block
    /// prefabs for each one. Returns a GameObject containing all these blocks,
    /// appropriately scaled and positioned in the center of the screen.
    /// </summary>
    public GameObject Load(Texture2D levelTex)
    {
        if (levelTex == null) throw new ArgumentNullException("level", "Level texture cannot be null.");
        GameObject levelContainer = new GameObject("LevelContainer");

        for (int x = 0; x < levelTex.width; x++)
        {
            for (int y = 0; y < levelTex.height; y++)
            {
                Color color = levelTex.GetPixel(x, y);
                if (color.a == 0) continue; //ignore transparent pixels
                var block = Instantiate(BlockPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                block.GetComponent<SpriteRenderer>().color = color;
                block.transform.parent = levelContainer.transform;
                BlockCount++;
            }
        }

        //scale the level to be SIZE world units large
        const float SIZE = 5.0f;
        float scaledWidth = SIZE / levelTex.width;
        float scaledHeight = SIZE / levelTex.height;
        levelContainer.transform.localScale = new Vector2(scaledWidth, scaledHeight);

        //center the level
        levelContainer.transform.position = new Vector2(-(scaledWidth * (levelTex.width - 1)) / 2, -(scaledHeight * (levelTex.height - 1)) / 2);

        return levelContainer;
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

    //TODO: pool blocks instead of Instantiate/Destroy?
}
