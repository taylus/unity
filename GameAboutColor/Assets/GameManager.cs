using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Keeps track of the current state of the game, and manages objects within it accordingly.
/// </summary>
public class GameManager : MonoBehaviour
{
    //TODO: make this manage the whole game
    //pick a current color the player should click
    //every few seconds, switch it
    //rack up combos if the player keeps getting the color right

    /// <summary>
    /// The game's current color that the player should click.
    /// </summary>
    internal static NamedColor CurrentColor;

    /// <summary>
    /// The UI text element on which to display the game's current color.
    /// </summary>
    public Text CurrentColorText;

    //private int currentScore;
    //private int scoreGoal;

    /// <summary>
    /// A reference to the game's <see cref="BallManager"/>.
    /// </summary>
    public BallManager Balls;

    /// <summary>
    /// Called once, when this script is enabled.
    /// </summary>
    public void Start()
    {
        CurrentColor = NamedColors.GetRandom();
        CurrentColorText.text = CurrentColor.Name;
        CurrentColorText.color = CurrentColor.Color;
        if (Balls == null) Balls = FindObjectOfType<BallManager>();
        Balls.RandomizeBallColors();
    }

    /// <summary>
    /// Called every frame when this script is enabled.
    /// </summary>
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }

    /// <summary>
    /// Quit the game, stopping play mode if we're in the Unity editor.
    /// </summary>
    private void Quit()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
