using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardSlot : MonoBehaviour
{
    public TextMeshProUGUI cardManacostSlot, cardNameSlot;
    public Image cardImage,cardTemplate;
    Card card;
    [HideInInspector]
    public bool holdCard = false;
    [SerializeField]
    AudioClip OnPointerEnterClip = null, OnCardPlayClip = null;

    CardFlyAnimation cardFly;
    public void CardSlotClear()
    {
        holdCard = false;
        cardImage.sprite = null;
        cardManacostSlot.text = "";
        cardNameSlot.text = "";
        SetCardFly(null);
        SetDisplay(false);
        
    }
    public void SetCardFly(CardFlyAnimation cardFlyNew)
    {
        if (cardFly != null)
            cardFly.DestroyThisCardFly();
        cardFly = cardFlyNew;

    }
    public void SetDisplay(bool isEnabled)
    {  
        cardManacostSlot.enabled = isEnabled;
        cardNameSlot.enabled = isEnabled;
        cardImage.enabled = isEnabled;
        cardTemplate.enabled = isEnabled;
    }
    public void SetCard(Card card)
    {
        this.card = card;
        holdCard = true;
        cardImage.sprite = card.cardSprite;
        cardManacostSlot.text = card.manaCost.ToString();
        cardNameSlot.text = card.nameText;
        

    }
    public void OnMouseEnterFunction()
    {
        CardFullSizeDisplay.instance.StartDisplayCard(card);
        AudioController.instance.PlayEffect(OnPointerEnterClip);
    }
    public void OnMouseExitFunction()
    {
        CardFullSizeDisplay.instance.StopDisplayCard(card);
    }
    public void OnMouseClickFunction()
    {
        
        if(BattleController.instance.PlayerCanPlayCard())
        if (BattleController.instance.UseMana(card.manaCost))
        {
            PlayerInformation.instance.playerHealthAndEffects.OnCardPlayStatusUpdate();
            CardFullSizeDisplay.instance.StopDisplayCard(card);
            Card cardTemp = card;
            BattleHandOfCards.instance.ClearCard(this);
            cardTemp.onPlay(PlayerInformation.instance.playerHealthAndEffects);
            AudioController.instance.PlayEffect(OnCardPlayClip);

        }

    }
}
