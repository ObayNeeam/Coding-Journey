using System;
using System.Collections;
using UnityEngine;

public static class Extentions
{
    public static void EnableCanvasGroup(this CanvasGroup group)
    {
        group.alpha = 1.0f;
        group.blocksRaycasts = true;
        group.interactable = true;
    }
    public static void DisbaleCanvasGroup(this CanvasGroup group)
    {
        group.alpha = 0.0f;
        group.blocksRaycasts = false;
        group.interactable = false;
    }
    public static void TweenScaleX(this MonoBehaviour mono, float to, float duration, Action onComplete = null)
    {
        mono.StartCoroutine(Tweener.TweenScaleXCoroutine(mono.transform, to, duration, onComplete));
    }
    public static IEnumerator Seq_TweenScaleX(this MonoBehaviour mono, float to, float duration, Action onComplete = null)
    {
        return Tweener.TweenScaleXCoroutine(mono.transform, to, duration, onComplete);
    }
}
