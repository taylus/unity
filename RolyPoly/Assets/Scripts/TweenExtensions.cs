using UnityEngine;
using System.Collections;

public static class TweenExtensions
{
    public static IEnumerator MoveTo(this MonoBehaviour obj, Vector3 location, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = obj.transform.position;
        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startingPosition, location, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator Scale(this MonoBehaviour obj, Vector3 scale, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingScale = obj.transform.localScale;
        while (elapsedTime < duration)
        {
            obj.transform.localScale = Vector3.Lerp(startingScale, scale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator Scale(this MonoBehaviour obj, float scale, float duration)
    {
        return Scale(obj, new Vector3(scale, scale, scale), duration);
    }
}
