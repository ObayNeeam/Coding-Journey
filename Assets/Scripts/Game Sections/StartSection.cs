using UnityEngine;

/// <summary>
/// start section will be responsible  on setting the game settings or loading a previous seassion from the desk
/// </summary>
public class StartSection : GameSectionBase
{
    protected override void Start()
    {
        base.Start();
        sectionWidget.OnWidgetEnd.AddListener(DisableSection);
    }
    public override void DisableSection()
    {
        // call the game manager to go to the next section
        Debug.Log("go to Next Section");
    }

    public override void EnableSection()
    {
        sectionWidget.EnableWidget();
    }
}
