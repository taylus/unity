using UnityEngine;

/// <summary>
/// Manages the <see cref="Ball"/> objects in the game.
/// </summary>
public class BallManager : MonoBehaviour
{
    //TODO: write "load a level of size m x n" logic

    /// <summary>
    /// Randomizes the colors of all <see cref="Ball"/> children under the
    /// game object this script is attached to.
    /// </summary>
    public void RandomizeBallColors()
    {
        foreach (Transform child in transform)
        {
            Ball ball = child.GetComponent<Ball>();
            ball.Color = NamedColors.GetRandom();
        }
    }
}
