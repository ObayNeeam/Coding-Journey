
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
        sectionWidget.OnSectionEnd+= EndSection;
    }

    public override void EnableSection()
    {
        sectionEnabled = true;
        sectionWidget.EnableSection();
    }
    private void EndSection(bool advance)
    {
        OnSectionEnd?.Invoke(advance);
    }
    public override void DisableSection()
    {
        sectionEnabled = false;
        sectionWidget.DisableSection();
    }
}
