using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Ball : MonoBehaviour
{
    private new Renderer renderer;
    private NamedColor color;
    public NamedColor Color
    {
        get { return color; }
        set
        {
            color = value;
            renderer.material.color = value.Color;
        }
    }

    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void OnMouseDown()
    {
        const float duration = 0.2f;
        StartCoroutine(FadeOut(duration));
        StartCoroutine(Scale(1.5f, duration));
        Destroy(gameObject, duration);
    }

    /// <summary>
    /// Fades this object out over <paramref name="duration"/> seconds.
    /// </summary>
    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0;
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
    private IEnumerator Scale(float scale, float duration)
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
}
