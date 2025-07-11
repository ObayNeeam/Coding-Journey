using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplaySection : GameSectionBase<GameplayWidget>
{
    [SerializeField, Range(1f, 10f)] private float cardTimer = 5f;

    private Dictionary<int, GameObject> visualOpenCards;
    private Dictionary<int, GameObject> stateOpenCards;
    private Dictionary<int, float> openCardsTimer;
    GameStateData gameStateData;
    private int playerMatches;
    private int playerClicks;
    private List<int> cardsData;
    private bool[] cardsState;
    public override event Action<bool> OnSectionEnd;
    public override void DisableSection(bool goNext)
    {
    }

    public override void EnableSection()
    {
        PrepareData();
    }
    private void PrepareData()
    {
        int totalCards = Mathf.RoundToInt(gameStateData.layout.x * gameStateData.layout.y);
        cardsData = new List<int>();
        cardsState = new bool[totalCards];
        cardsData = PopulateValues(totalCards);
        gameStateData.cellsState = cardsState.ToArray();
        gameStateData.cellsType = cardsData.ToArray();

        visualOpenCards = new Dictionary<int, GameObject>();
        stateOpenCards = new Dictionary<int, GameObject>();
        openCardsTimer = new Dictionary<int, float>();
    }
    private List<int> PopulateValues(int totalCards)
    {
        List<int> possiableIndexes = Enumerable.Range(0, totalCards).ToList();
        List<int> possiableValue = Enumerable.Range(0, (totalCards / 2)).ToList();

        int[] cardsValues = new int[totalCards];

        for (int i = 0; i < possiableValue.Count; i++)
        {
            int firstIndex = UnityEngine.Random.Range(0, possiableIndexes.Count);
            cardsValues[possiableIndexes[firstIndex]] = possiableValue[i];
            possiableIndexes.RemoveAt(firstIndex);

            int secondIndex = UnityEngine.Random.Range(0, possiableIndexes.Count);
            cardsValues[possiableIndexes[secondIndex]] = possiableValue[i];
            possiableIndexes.RemoveAt(secondIndex);
        }
        return cardsValues.ToList();
    }
}
