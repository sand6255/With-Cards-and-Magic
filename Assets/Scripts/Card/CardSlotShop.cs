using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class CardSlotShop : MonoBehaviour
{
    public TextMeshProUGUI cardManacostSlot, cardNameSlot;
    public Image cardImage, cardTemplate;
    Card card;
    [SerializeField]
    AudioClip OnPointerEnterClip = null, OnCardPlayClip = null;
    public void CardSlotClear()
    {
        cardImage.sprite = null;
        cardManacostSlot.text = "";
        cardNameSlot.text = "";
        SetDisplay(false);

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
        cardImage.sprite = card.cardSprite;
        cardManacostSlot.text = card.manaCost.ToString();
        cardNameSlot.text = card.nameText;


    }
    public void OnMouseEnterFunction()
    {
        CardFullSizeDisplayShop.instance.StartDisplayCard(card);
        AudioController.instance.PlayEffect(OnPointerEnterClip);
    }
    public void OnMouseExitFunction()
    {
        CardFullSizeDisplayShop.instance.StopDisplayCard(card);
    }
    public void OnMouseClickFunction()
    {

        if (PlayerInformation.instance.UseWindShards(ShopController.instance.cardCost))
        {
            ShopController.instance.BoughtCard();
            CardFullSizeDisplayShop.instance.StopDisplayCard(card);
            Card cardTemp = card;
            this.CardSlotClear();
            PlayerInformation.instance.AddCardToDeck(cardTemp);
            AudioController.instance.PlayEffect(OnCardPlayClip);
        }

    }
}
