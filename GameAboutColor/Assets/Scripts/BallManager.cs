using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the <see cref="Ball"/> objects in the game.
/// </summary>
public class BallManager : MonoBehaviour
{
    /// <summary>
    /// The prefab game object that is used for spawning balls.
    /// </summary>
    public GameObject BallPrefab;

    /// <summary>
    /// The board on which balls are spawned.
    /// </summary>
    public Transform GameBoard;

    /// <summary>
    /// Creates width x height balls of random colors from the given set and
    /// attaches them as children to this script's game object.
    /// </summary>
    public void LoadLevel(int width, int height, List<NamedColor> colors)
    {
        if (width < 1) throw new ArgumentException("Width of balls to spawn must be > 0.", "width");
        if (height < 1) throw new ArgumentException("Height of balls to spawn must be > 0.", "height");

        DestroyAllChildren();

        //TODO: scale balls down to fit if needed

        var board = new
        {
            left = GameBoard.position.x - (GameBoard.localScale.x / 2),
            right = GameBoard.position.x + (GameBoard.localScale.x / 2),
            top = GameBoard.position.z - (GameBoard.localScale.z / 2),
            bottom = GameBoard.position.z + (GameBoard.localScale.z / 2)
        };
        var cell = new
        {
            width = GameBoard.localScale.x / width,
            height = GameBoard.localScale.z / height
        };
        for (float x = board.left; x < board.right; x += cell.width)
        {
            for (float z = board.top; z < board.bottom; z += cell.height)
            {
                var rect = new Rect(x, z, cell.width, cell.height);
                var ball = Instantiate(BallPrefab, new Vector3(rect.center.x, 0.5f, rect.center.y), Quaternion.identity) as GameObject;
                ball.transform.parent = transform;
                //Debug.LogFormat("Spawning {0} at ({1}, {2})", ball, x, z);
            }
        }

        RandomizeBallColors();
    }

    /// <summary>
    /// Destroys all child game objects attached to this script's game object.
    /// </summary>
    /// <remarks>That name, though.</remarks>
    private void DestroyAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Randomizes the colors of all <see cref="Ball"/> children attached
    /// to this script's game object.
    /// </summary>
    private void RandomizeBallColors()
    {
        foreach (Transform child in transform)
        {
            Ball ball = child.GetComponent<Ball>();
            ball.Color = NamedColors.GetRandom();
        }
    }

    /// <summary>
    /// Resets all disabled <see cref="Ball"/> children attached to this
    /// script's game object. To be called when a player's combo ends.
    /// </summary>
    public void ResetDisabledBalls()
    {
        foreach (Transform child in transform)
        {
            Ball ball = child.GetComponent<Ball>();
            if (!ball.enabled) ball.Reset(NamedColors.GetRandom());
        }
    }

    /// <summary>
    /// Returns true if there are any balls of the given color attached to this
    /// script's game object, false otherwise.
    /// </summary>
    /// <returns></returns>
    public bool ContainsAnyBallsWithColor(NamedColor color)
    {
        foreach (Transform child in transform)
        {
            Ball ball = child.GetComponent<Ball>();
            if (ball.enabled && ball.Color.Name == color.Name) return true;
        }

        return false;
    }
}
