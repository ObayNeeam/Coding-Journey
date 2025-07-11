using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// base scipt for all widgets in the app
/// </summary>
public abstract class WidgetBase : MonoBehaviour, ISection
{
    /// <summary>
    /// An event for letting the logic section class know when to move on from to the next section
    /// </summary>
    public abstract event Action<bool> OnSectionEnd;

    public abstract void DisableSection(bool advance);

    public abstract void EnableSection();
}
