using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWidget : WidgetBase
{
    /// we don't need to do anything in this widget
    public override event Action<bool> OnSectionEnd;
    public override void DisableSection()
    {
        sectionGroup.DisbaleCanvasGroup();
    }

    public override void EnableSection()
    {
        sectionGroup.EnableCanvasGroup();
    }
}
