using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base scipt for all widgets in the app
/// </summary>
public abstract class WidgetBase : MonoBehaviour
{
    public abstract void EnableWidget();
    public abstract void DisableWidget();
}
