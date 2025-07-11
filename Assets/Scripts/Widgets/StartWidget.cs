using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartWidget : WidgetBase
{
    [SerializeField] private GameObject loadGameBtn;
    [SerializeField] private GameObject newGameOptionsSection;
    [SerializeField] private GameObject mainStartSection;
    [SerializeField] private TMP_Dropdown gridSizeDropdown;

    public override event Action<bool> OnSectionEnd;

    public override void DisableSection(bool goNext)
    {
        // when we disable the section we reset the values
        mainStartSection.SetActive(true);
        newGameOptionsSection.SetActive(false);
    }

    public override void EnableSection()
    {
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
    }
}
