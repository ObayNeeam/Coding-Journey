using Unity.VisualScripting;
using UnityEngine;

public abstract class GameSectionBase : MonoBehaviour
{
    [SerializeField] protected WidgetBase sectionWidget;
    protected virtual void Start() {}
    /// <summary>
    /// this method will be responsiable for enabling a certain game section when called
    /// </summary>
    public abstract void EnableSection();
    /// <summary>
    /// this method will be responsiable for disabling a certain game section when called
    /// </summary>
    public abstract void DisableSection();
}
