using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardFullSizeDisplay : MonoBehaviour
{
    public TextMeshProUGUI cardManacostSlot, cardNameSlot, cardTextSlot;
    public Image cardImage, cardTemplate;
    public static CardFullSizeDisplay instance = null;
    [SerializeField]
    StatusDisplayDetailsSpawner statusDisplayDetailsSpawner = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        SetDisplay(false);
    }
    public void StartDisplayCard(Card card)
    {
        SetDisplay(true);
        SetCard(card);
    }
    public void StopDisplayCard(Card card)
    {
        SetDisplay(false);
        statusDisplayDetailsSpawner.StopDisplaying();
    }
    void SetDisplay(bool isEnabled)
    {
        cardManacostSlot.enabled = isEnabled;
        cardNameSlot.enabled = isEnabled;
        cardImage.enabled = isEnabled;
        cardTemplate.enabled = isEnabled;
        cardTextSlot.enabled = isEnabled;
    }
    void SetCard(Card card)
    {
        cardImage.sprite = card.cardSprite;
        cardManacostSlot.text = card.manaCost.ToString();
        cardNameSlot.text = card.nameText;
        cardTextSlot.text = card.GetCardText(PlayerInformation.instance.playerHealthAndEffects, statusDisplayDetailsSpawner);

    }
}
