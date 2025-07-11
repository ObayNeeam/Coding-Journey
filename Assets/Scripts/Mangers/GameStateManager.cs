using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameSectionsObj;
    private List<ISection> gameSections;
    private int currentSectionIndex;
    // Start is called before the first frame update
    void Start()
    {
        // our game entry point
        // we enable the first section in the game
        GetGameSections();
        currentSectionIndex = 0;
        EnableSection(currentSectionIndex);
    }
    private void GetGameSections()
    {
        gameSections = new();
        foreach (GameObject section in gameSectionsObj)
        {
            gameSections.Add(section.GetComponent<ISection>());
        }
    }
    private void EnableSection(int sectionIndex)
    {
        gameSections[sectionIndex].OnSectionEnd += GoSection;
        gameSections[sectionIndex].EnableSection();
    }
    private void DisableSection(int sectionIndex)
    {
        gameSections[sectionIndex].DisableSection();
        gameSections[sectionIndex].OnSectionEnd-=GoSection;
    }
    public void GoSection(bool advanceTo)
    {
        DisableSection(currentSectionIndex);
        currentSectionIndex = advanceTo? GetNextIndex() : GetPreviousIndex();
        EnableSection(currentSectionIndex);
    }
    private int GetNextIndex()
    {
        if (currentSectionIndex + 1 < gameSections.Count) return currentSectionIndex + 1;
        else return 0;
    }
    private int GetPreviousIndex()
    {
        if (currentSectionIndex - 1 >= 0) return currentSectionIndex + 1;
        else return gameSections.Count - 1;
    }
}
