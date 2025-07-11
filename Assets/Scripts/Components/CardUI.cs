using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image mainImage;
    [SerializeField] private Image childImage;
    [SerializeField] private Button cardBtn;

    public int CardType => cardType;
    public int CardIndex => cardIndex;
    public event System.Action<CardUI> OnCardClick;

    private int cardType;
    private int cardIndex;
    private Sprite flipSprite;

    public void SetCardData(int index, int type, Sprite flipSprite, bool revealed)
    {
        cardType = type;
        cardIndex = index;
        this.flipSprite = flipSprite;
        childImage.sprite = flipSprite;
        if (revealed)
        {
            mainImage.enabled = false;
            childImage.enabled = true;
            cardBtn.interactable = false;
        }
        else
        {
            mainImage.enabled = true;
            childImage.enabled = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!cardBtn.interactable) return;
        OnCardClick?.Invoke(this);
    }
    public void FlipCard(bool reveal, float tweenTime)
    {
        if (reveal)
        {
            mainImage.enabled = false;
            childImage.enabled = true;
        }
        else
        {
            mainImage.enabled = true;
            childImage.enabled = false;
        }
    }
    public void SetBtnInteractable(bool state)
    {
        cardBtn.interactable = state;
    }
}
