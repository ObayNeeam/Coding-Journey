using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayWidget : WidgetBase
{
    public override event Action<bool> OnSectionEnd;

    public override void DisableSection(bool goNext)
    {
        throw new System.NotImplementedException();
    }

    public override void EnableSection()
    {
        throw new System.NotImplementedException();
    }
}
