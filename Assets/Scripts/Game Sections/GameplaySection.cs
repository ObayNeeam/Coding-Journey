using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplaySection : GameSectionBase<GameplayWidget>, ISection
{
    [SerializeField, Range(1f, 10f)] private float cardTimer = 5f;

    private Dictionary<int, CardUI> visualOpenCards;
    private Dictionary<int, CardUI> stateOpenCards;
    private Dictionary<int, float> openCardsTimer;
    GameStateData gameStateData;
    private List<int> cardsData;
    private bool[] cardsState;
    public override event Action<bool> OnSectionEnd;
    protected override void Start()
    {
        sectionWidget.OnUICardClicked += OnCardPick;
        sectionWidget.OnSectionEnd += EndSection;
    }
    public override void DisableSection()
    {
        sectionEnabled = false;
        sectionWidget.DisableSection();
    }

    public override void EnableSection()
    {
        sectionEnabled = true;
        gameStateData = GameDataManager.Instance.GameState;
        PrepareData();
        sectionWidget.EnableSection();
    }
    private void EndSection(bool advanceTo)
    {
        if (!advanceTo)
        {
            GameDataManager.Instance.OverrideState(gameStateData);
            GameDataManager.Instance.SaveState();
        }
            OnSectionEnd(false);
    }
    private void Update()
    {
        if (!sectionEnabled) return;

        if (openCardsTimer.Count <= 0) return;

        List<int> cardsToRemove = new List<int>();
        List<int> cardsKeys = new List<int>(openCardsTimer.Keys);
        foreach (int key in cardsKeys)
        {
            if (cardsState[key]) continue;

            openCardsTimer[key] -= Time.deltaTime;

            if (openCardsTimer[key] <= 0) cardsToRemove.Add(key);
        }
        RemoveIdleCards(cardsToRemove);
    }
    private void RemoveIdleCards(List<int> cardsToRemove)
    {
        foreach (int index in cardsToRemove)
        {
            openCardsTimer.Remove(index);
            if (stateOpenCards.ContainsKey(index)) stateOpenCards.Remove(index);
            CardUI card = visualOpenCards[index];
            visualOpenCards.Remove(index);
            card.FlipCard(false, 0.25f);
            card.SetBtnInteractable(true);
        }
    }
    private void PrepareData()
    {
        int totalCards = Mathf.RoundToInt(gameStateData.layout.x * gameStateData.layout.y);
        cardsData = new List<int>();
        cardsState = new bool[totalCards];

        if (!GameDataManager.Instance.SavedState)
        {
            cardsData = PopulateValues(totalCards);
            gameStateData.cellsState = cardsState.ToArray();
            gameStateData.cellsType = cardsData.ToArray();
            GameDataManager.Instance.OverrideState(gameStateData);
        }
        else
        {
            cardsData = gameStateData.cellsType.ToList();
            cardsState = gameStateData.cellsState.ToArray();
            //playerClicks = gameStateData.userClicks;
            //playerMatches = gameStateData.userMatches;
        }

        sectionWidget.SetPlayerClicks(gameStateData.userClicks);
        sectionWidget.SetPlayerScore(gameStateData.userMatches);
        sectionWidget.SetPlayerCombo(gameStateData.combos);
        
        visualOpenCards = new Dictionary<int, CardUI>();
        stateOpenCards = new Dictionary<int, CardUI>();
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
    private void OnCardPick(CardUI card)
    {
        card.SetBtnInteractable(false);
        AudioManager.Instance.PlayCardFlip();
        //playerClicks++;
        gameStateData.userClicks++;
        sectionWidget.SetPlayerClicks(gameStateData.userClicks);

        card.FlipCard(true, 0.25f, () => {
            visualOpenCards.Add(card.CardIndex, card);
            stateOpenCards.Add(card.CardIndex, card);
            openCardsTimer.Add(card.CardIndex, cardTimer);
            CheckOpenCards();
        });
    }
    private void CheckOpenCards()
    {
        //if (stateOpenCards.Count <= 1) return;

        List<int> keys = new List<int>(stateOpenCards.Keys);
        for (int i = 0; i < keys.Count; i += 2)
        {
            if (i + 1 >= keys.Count)
            {
                //stateOpenCards.Remove(keys[i]);
                break;
            }

            int key1 = keys[i];
            int key2 = keys[i + 1];
            CardUI card1 = stateOpenCards[key1];
            CardUI card2 = stateOpenCards[key2];
            //if (openCards[key].CardType == card.CardType && openCards[key].CardIndex != card.CardIndex && !cardsState[key])
            if (IsOpenValid(card1, card2))
            {
                Debug.Log($"A Match Index {card1.CardIndex} and {card2.CardIndex} | Type {card1.CardType}");
                AudioManager.Instance.PlayCardMatch();
                HandleMatchingCards(key1, key2);
                continue;
            }
            else
            {
                stateOpenCards.Remove(key1);
                stateOpenCards.Remove(key2);
                gameStateData.combos = 0;
                sectionWidget.SetPlayerCombo(gameStateData.combos);
                AudioManager.Instance.PlayCardMismatch();
            }
        }
    }
    private bool IsOpenValid(CardUI card1, CardUI card2)
    {
        return 
            card1.CardType == card2.CardType &&
            card1.CardIndex != card2.CardIndex &&
            !cardsState[card1.CardIndex];
    }
    private void HandleMatchingCards(int key1, int key2)
    {
        cardsState[key1] = true;
        cardsState[key2] = true;

        gameStateData.cellsState = cardsState;
        //playerMatches++;
        gameStateData.userMatches++;

        sectionWidget.SetPlayerScore(gameStateData.userMatches);

        openCardsTimer.Remove(key1);
        openCardsTimer.Remove(key2);
        CheckCombo();
        gameStateData.lastCorrectMatch = gameStateData.userClicks;
        stateOpenCards[key1].SetBtnInteractable(false);
        stateOpenCards[key2].SetBtnInteractable(false);

        stateOpenCards.Remove(key1);
        stateOpenCards.Remove(key2);

        visualOpenCards.Remove(key1);
        visualOpenCards.Remove(key2);

        CheckGameEnding();
    }
    private void CheckCombo()
    {
        if (gameStateData.lastCorrectMatch != 0)
        {
            if (gameStateData.userClicks - gameStateData.lastCorrectMatch == 2)
            {
                gameStateData.combos++;

            }
            sectionWidget.SetPlayerCombo(gameStateData.combos);
        }
    }
    private void CheckGameEnding()
    {
        foreach (bool cellState in cardsState)
        {
            if (!cellState) return;
        }
        EndGame();
    }
    private void EndGame()
    {
        GameDataManager.Instance.DeleteState();
        // deal with game logic
        OnSectionEnd?.Invoke(true);
    }
}
