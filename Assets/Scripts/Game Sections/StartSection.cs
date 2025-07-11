
using System;

/// <summary>
/// start section will be responsible  on setting the game settings or loading a previous seassion from the desk
/// </summary>
public class StartSection : GameSectionBase<StartWidget>
{
    public override event Action<bool> OnSectionEnd;

    protected override void Start()
    {
        base.Start();
        sectionWidget.OnSectionEnd+=DisableSection;
    }

    public override void EnableSection()
    {
        sectionWidget.EnableSection();
    }

    public override void DisableSection(bool advance)
    {
        throw new System.NotImplementedException();
    }
}
