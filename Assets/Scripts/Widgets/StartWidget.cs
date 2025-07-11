using System;
using TMPro;
using UnityEngine;

public class StartWidget : WidgetBase
{
    [SerializeField] private GameObject loadGameBtn;
    [SerializeField] private GameObject newGameOptionsSection;
    [SerializeField] private GameObject mainStartSection;
    [SerializeField] private TMP_Dropdown gridSizeDropdown;

    public override event Action<bool> OnSectionEnd;
    public override void DisableSection()
    {
        // when we disable the section we reset the values
        sectionGroup.DisbaleCanvasGroup();
        mainStartSection.SetActive(true);
        newGameOptionsSection.SetActive(false);
    }

    public override void EnableSection()
    {
        OnClick_GameGridSelection(0);
        sectionGroup.EnableCanvasGroup();
        // check if there is an old game session we can continue
        //if(check if there is an old game session)
        loadGameBtn.SetActive(true);
        //if not then
    }

    public void OnClick_LoadGameBtn()
    {
        // we will call another script to handle passing the data from an old session to the next section

        // we call start game func because we pass the data to another script that will handle presistent data
        OnClick_StartGame();
    }
    /// <summary>
    /// open the new game settings sub section
    /// </summary>
    public void OnClick_NewGameBtn()
    {
        mainStartSection.SetActive(false);
        newGameOptionsSection.SetActive(true);
    }
    public void OnClick_StartGame()
    {
        OnSectionEnd?.Invoke(true);
    }
    public void OnClick_GameGridSelection(int optionIndex)
    {
        // we get the option text string
        string value = gridSizeDropdown.options[optionIndex].text;
        // we parse the value here in a helper function to numbers and pass it along to the next section
        ParseLayoutString(value);
    }
    private void ParseLayoutString(string value)
    {
        int.TryParse(value[0].ToString(),out int rows);
        int.TryParse(value[2].ToString(), out int col);
        GameDataManager.Instance.SetLayoutState(rows, col);
    }
}
