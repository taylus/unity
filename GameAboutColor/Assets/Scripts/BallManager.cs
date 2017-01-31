using UnityEngine;

/// <summary>
/// Manages the <see cref="Ball"/> objects in the game.
/// </summary>
public class BallManager : MonoBehaviour
{
    //TODO: write "load a level of size m x n" logic

    /// <summary>
    /// Randomizes the colors of all <see cref="Ball"/> children attached
    /// to this script's game object.
    /// </summary>
    public void RandomizeBallColors()
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
}
