using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// A colored ball that the user clicks at the right time for points.
/// </summary>
[RequireComponent(typeof(Renderer), typeof(Collider))]
public class Ball : MonoBehaviour
{
    private GameManager gameManager;
    private new Renderer renderer;
    private NamedColor color;
    public NamedColor Color
    {
        get { return color; }
        set
        {
            if (renderer == null) renderer = GetComponent<Renderer>();
            color = value;
            renderer.material.color = value.Color;
        }
    }
    private Vector3 initialScale;

    /// <summary>
    /// Called once, when this script is enabled.
    /// </summary>
    public void Start()
    {
        initialScale = transform.localScale;
    }

    /// <summary>
    /// Fires when this object is clicked.
    /// </summary>
    public void OnMouseDown()
    {
        if (!enabled) return;
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        gameManager.BallClicked(this);
    }

    /// <summary>
    /// Fades this object out over <paramref name="duration"/> seconds.
    /// </summary>
    public IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0;
        if (renderer == null) renderer = GetComponent<Renderer>();
        float startingAlpha = renderer.material.color.a;
        Color color = renderer.material.color;
        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(startingAlpha, 0, (elapsedTime / duration));
            renderer.material.color = color;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        color.a = 0;
        renderer.material.color = color;
    }

    /// <summary>
    /// Scales this object's size over <paramref name="duration"/> seconds.
    /// </summary>
    public IEnumerator Scale(float scale, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingScale = transform.localScale;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startingScale, startingScale * scale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = startingScale * scale;
    }

    /// <summary>
    /// Resets this ball's scale and opacity, and sets it to the given color.
    /// </summary>
    public void Reset(NamedColor color)
    {
        Color = color;
        transform.localScale = initialScale;
        enabled = true;
    }
}
