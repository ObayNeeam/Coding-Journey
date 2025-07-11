using System.Collections;
using System;
using UnityEngine;

public class Tweener
{
    public static IEnumerator TweenScaleXCoroutine(Transform target, float toX, float duration, Action onComplete)
    {
        float fromX = target.localScale.x;
        float elapsed = 0f;
        Vector3 currentScale = target.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float newX = Mathf.Lerp(fromX, toX, t);
            target.localScale = new Vector3(newX, currentScale.y, currentScale.z);
            yield return null;
        }

        // Ensure exact target value
        target.localScale = new Vector3(toX, currentScale.y, currentScale.z);

        onComplete?.Invoke();
    }
}
