using System;
using UnityEngine;

public abstract class GameSectionBase<IWidget> : MonoBehaviour, ISection where IWidget : WidgetBase
{
    [SerializeField] protected IWidget sectionWidget;

    public abstract event Action<bool> OnSectionEnd;

    protected bool sectionEnabled;
    /// <summary>
    /// Pass true if you want to go to the next section
    /// </summary>
    protected virtual void Start() {}

    public abstract void EnableSection();
    /// <summary>
    /// this method will be responsiable for disabling a certain game section when called
    /// </summary>
    public abstract void DisableSection();
}
