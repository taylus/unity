﻿using UnityEngine;
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
    /// How many seconds remain until the current combo ends.
    /// </summary>
    private float untilNextColor;

    /// <summary>
    /// How long (in seconds) the player has, at maximum, to get combos between clicks.
    /// </summary>
    public float ComboTimerMax = 1.75f;

    /// <summary>
    /// How long (in seconds) the player has, at mimum, to get combos between clicks.
    /// </summary>
    /// <remarks>
    /// Successful combos will make the timer shorter and shorter, but it won't ever
    /// get shorter than this.
    /// </remarks>
    public float ComboTimerMin = 0.75f;

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
    /// The UI text element on which to display the game's debug text.
    /// </summary>
    public Text DebugText;

    /// <summary>
    /// Called once, when this script is enabled.
    /// </summary>
    public void Start()
    {
        CurrentColor = NamedColors.GetRandom();
        CurrentScore = 0;
        untilNextColor = ComboTimerMax;

        if (Balls == null) Balls = FindObjectOfType<BallManager>();
        Balls.RandomizeBallColors();
    }

    /// <summary>
    /// Called every frame when this script is enabled.
    /// </summary>
    public void Update()
    {
        if (DebugText != null)
        {
            //TODO: display a sliding bar so the player can see what the timer is at
            DebugText.text = string.Format("[Combo: {0}] [Next color in: {1:f2} sec]", currentCombo, untilNextColor);
        }

        if (Input.GetKey(KeyCode.Escape)) Quit();
        if(currentCombo > 0) untilNextColor -= Time.deltaTime;
        if(untilNextColor < 0)
        {
            ResetCombo();
            ResetColorTimer();
        }
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
        ResetColorTimer();

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

            //decrease color timer, to make longer combos harder to get
            untilNextColor -= currentCombo * 0.1f;

            //TODO: play good sound w/ increasing pitch per combo point
        }
        else
        {
            //bad input -> reset combo, current color, game board
            ResetCombo();

            //decrease score
            CurrentScore--;
            if (CurrentScore < 0) CurrentScore = 0;

            //TODO: play bad sound
        }
    }

    /// <summary>
    /// Resets the "next color in..." timer back to its maximum value.
    /// </summary>
    public void ResetColorTimer()
    {
        untilNextColor = ComboTimerMax;
    }

    /// <summary>
    /// Resets the combo count, target color, and balls in between combos.
    /// </summary>
    public void ResetCombo()
    {
        currentCombo = 0;
        CurrentColor = NamedColors.GetRandomExcept(CurrentColor);
        Balls.ResetDisabledBalls();
    }
}
