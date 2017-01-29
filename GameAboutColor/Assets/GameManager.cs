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
    /// Backing variable for <see cref="CurrentColor"/>.
    /// </summary>
    private NamedColor currentColor;

    /// <summary>
    /// The game's current color that the player should click.
    /// </summary>
    internal NamedColor CurrentColor
    {
        get { return currentColor; }
        private set
        {
            currentColor = value;
            if (CurrentColorText != null)
            {
                CurrentColorText.text = CurrentColor.Name;
                CurrentColorText.color = CurrentColor.Color;
            }
        }
    }

    /// <summary>
    /// The UI text element on which to display the game's current color.
    /// Set when <see cref="CurrentColor"/> is set.
    /// </summary>
    public Text CurrentColorText;

    /// <summary>
    /// Backing variable for <see cref="CurrentScore"/>.
    /// </summary>
    private int currentScore;

    /// <summary>
    /// The number of balls the player has clicked in their current combo.
    /// Used as a score multiplier.
    /// </summary>
    private int currentCombo;

    /// <summary>
    /// The player's current score.
    /// </summary>
    internal int CurrentScore
    {
        get { return currentScore; }
        private set
        {
            currentScore = value;
            if (ScoreText != null)
            {
                ScoreText.text = currentScore.ToString();
            }
        }
    }

    /// <summary>
    /// The UI text element on which to display the player's current score.
    /// Set when <see cref="CurrentScore"/> is set.
    /// </summary>
    public Text ScoreText;

    //how many points the player needs to clear this level
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
        CurrentScore = 0;

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

    /// <summary>
    /// Callback for when a ball is clicked.
    /// </summary>
    public void BallClicked(Ball ball)
    {
        if (ball.Color.Name == CurrentColor.Name)
        {
            //good input -> fade out and destroy the ball
            const float duration = 0.2f;
            StartCoroutine(ball.FadeOut(duration));
            StartCoroutine(ball.Scale(1.5f, duration));
            ball.enabled = false;

            //increment score
            currentCombo++;
            CurrentScore += currentCombo;

            //TODO: play good sound w/ increasing pitch per combo point
        }
        else
        {
            //bad input -> reset combo, current color, game board
            currentCombo = 0;
            CurrentColor = NamedColors.GetRandomExcept(CurrentColor);
            Balls.ResetDisabledBalls();

            //TODO: play bad sound
        }
    }
}
