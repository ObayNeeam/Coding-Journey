using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISection
{
    public event Action<bool> OnSectionEnd;
    /// <summary>
    /// this method will be responsiable for enabling a certain game section when called
    /// </summary>
    public void EnableSection();
    /// <summary>
    /// this method will be responsiable for disabling a certain game section when called
    /// </summary>
    public void DisableSection();
}
