using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandOfCards : MonoBehaviour
{
    public static BattleHandOfCards instance = null;
    [SerializeField]
    CardSlot[] cardSlots = null;
    [SerializeField]
    GameObject prefabCardFlyAnimation = null,cardInBattleSmall = null;
    [SerializeField]
    AudioClip onCardDrawClip = null;
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
    void Start()
    {
        InitializeManager();
    }
    private void InitializeManager()
    {

    }
    public bool AddCard(Card card)
    {
        foreach(CardSlot cardSlot in cardSlots)
            if(cardSlot.holdCard == false)
            {
                cardSlot.SetCard(card);
                GameObject currentFlyAnimation = Instantiate(prefabCardFlyAnimation, cardInBattleSmall.transform);
                AudioController.instance.PlayEffect(onCardDrawClip);
                if (card.manaCost <= BattleController.instance.currentMana)
                EndTurn.instance.SomethingToDoOnTurn();
                currentFlyAnimation.GetComponent<CardFlyAnimation>().SetEndPointAndCardSlot(card, cardSlot);
                return true;
            }
        return false;
    }
    public void ClearHand()
    {
        foreach (CardSlot cardSlot in cardSlots)
        {
            ClearCard(cardSlot);
        }
        EndTurn.instance.SomethingToDoOnTurn();
    }
    
    public void ClearCard(CardSlot cardSlot)
    {
        cardSlot.CardSlotClear();
        bool isNothingToDo = true;
        foreach(CardSlot cardSlot2 in cardSlots)
        {
             int parse;
            if (cardSlot2.holdCard && int.TryParse(cardSlot2.cardManacostSlot.text, out parse) && parse <= BattleController.instance.currentMana)
                isNothingToDo = false;
        }
        if (isNothingToDo)
            EndTurn.instance.NothingToDoOnTurn();
        else
            EndTurn.instance.SomethingToDoOnTurn();

    }
    
}

