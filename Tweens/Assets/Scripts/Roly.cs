using UnityEngine;
using System.Collections;

public class Roly : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(Randomize(2.0f));
    }

    protected IEnumerator Randomize(float duration)
    {
        while (true)
        {
            float scale = Random.Range(0.5f, 2.0f);
            StartCoroutine(MoveTo(new Vector2(Random.Range(-8, 8) / scale, Random.Range(-5, 5) / scale), duration));
            StartCoroutine(Rotate(new Vector3(0, 0, Random.Range(-360, 360)), duration));
            StartCoroutine(Scale(new Vector2(scale, scale), duration));
            yield return new WaitForSeconds(duration);
        }
    }

    protected IEnumerator MoveTo(Vector3 location, float duration)
    {
        Debug.Log(string.Format("MoveTo({0}, {1}", location, duration));
        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, location, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator MoveRelative(Vector3 offset, float duration)
    {
        return MoveTo(transform.position + offset, duration);
    }

    protected IEnumerator Rotate(Vector3 rotation, float duration)
    {
        Debug.Log(string.Format("Rotate({0}, {1}", rotation, duration));
        float elapsedTime = 0;
        Vector3 startingRotation = transform.eulerAngles;
        while (elapsedTime < duration)
        {
            transform.eulerAngles = Vector3.Lerp(startingRotation, rotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator Scale(Vector3 scale, float duration)
    {
        Debug.Log(string.Format("Scale({0}, {1}", scale, duration));
        float elapsedTime = 0;
        Vector3 startingScale = transform.localScale;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startingScale, scale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
