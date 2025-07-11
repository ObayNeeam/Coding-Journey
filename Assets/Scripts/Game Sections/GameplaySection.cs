using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySection : GameSectionBase<GameplayWidget>
{
    public override event Action<bool> OnSectionEnd;
    public override void DisableSection(bool goNext)
    {
    }

    public override void EnableSection()
    {
        
    }
}
