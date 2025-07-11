using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;

public class GameplayWidget : WidgetBase
{
    [SerializeField] private RectTransform cardContainer;
    [SerializeField] private GridLayoutGroup cardsGridLayout;
    [SerializeField] private float gridElementsSpace;
    [SerializeField] private int gridElementsPadding;
    [SerializeField] private CardUI cardPrefab;
    [SerializeField] private List<Sprite> cardsSpriteTypes;

    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI playerClicks;

    public event Action<CardUI> OnUICardClicked;
    public event Action OnHomeBtnPressed;

    public override event Action<bool> OnSectionEnd;

    List<CardUI> cards;

    public override void DisableSection()
    {
        sectionGroup.DisbaleCanvasGroup();
    }

    public override void EnableSection()
    {
        GameStateData state = GameDataManager.Instance.GameState;

        ConfigureGrid(state);
        BuildGrid(state.cellsType, state.cellsState);
        sectionGroup.EnableCanvasGroup();
    }
    public void BuildGrid(int[] cardsData, bool[] cardsStates)
    {
        cards = new List<CardUI>();
        for (int i = 0; i < cardsData.Length; i++)
        {
            CardUI card = Instantiate(cardPrefab, cardContainer);
            int cardType = cardsData[i];

            card.SetCardData(i, cardType, cardsSpriteTypes[cardType], cardsStates[i]);

            cards.Add(card);
            card.OnCardClick += OnCardClicked;
        }
    }
    private void ConfigureGrid(GameStateData state)
    {
        cardsGridLayout.spacing = new Vector2(gridElementsSpace, gridElementsSpace);
        cardsGridLayout.padding = new RectOffset(gridElementsPadding, gridElementsPadding, gridElementsPadding, gridElementsPadding);

        RectTransform gridRect = (cardsGridLayout.transform as RectTransform);
        Vector2 layout = state.layout;
        if (layout.x == layout.y)
        {
            cardsGridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            cardsGridLayout.constraintCount = Mathf.RoundToInt(layout.x);
        }
        if (layout.x > layout.y)
        {
            cardsGridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            cardsGridLayout.constraintCount = Mathf.RoundToInt(layout.x);
        }
        else
        {
            cardsGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            cardsGridLayout.constraintCount = Mathf.RoundToInt(layout.y);
        }
        float cellHeight = (gridRect.rect.height - (gridElementsPadding * 2) - ((layout.y - 1) * gridElementsSpace)) / layout.y;
        cardsGridLayout.cellSize = new Vector2(cellHeight, cellHeight);

    }

    private void OnCardClicked(CardUI card)
    {
        OnUICardClicked?.Invoke(card);
    }
    public void OnHomeBtnClick()
    {
        OnSectionEnd?.Invoke(false);
    }
    public void SetPlayerScore(int value)
    {
        playerScore.text = value.ToString();
    }
    public void SetPlayerClicks(int value)
    {
        playerClicks.text = value.ToString();
    }
}
