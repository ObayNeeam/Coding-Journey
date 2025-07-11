using UnityEngine;

public abstract class GameSectionBase : MonoBehaviour
{
    /// <summary>
    /// this method will be responsiable for enabling a certain game section when called
    /// </summary>
    public abstract void EnableSection();
    /// <summary>
    /// this method will be responsiable for disabling a certain game section when called
    /// </summary>
    public abstract void DisableSection();
}
