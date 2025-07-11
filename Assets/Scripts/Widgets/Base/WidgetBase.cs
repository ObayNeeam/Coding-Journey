using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// base scipt for all widgets in the app
/// </summary>
public abstract class WidgetBase : MonoBehaviour
{
    public UnityEvent OnWidgetEnd;
    public abstract void EnableWidget();
    public abstract void DisableWidget();
}
