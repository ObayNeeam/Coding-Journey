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
}
