using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
    public static ShopController instance = null;
    [SerializeField]
    int basicCardCost = 50;
    public int cardCost = 50;
    [SerializeField]
    AudioClip onArrowPointerEnterClip = null, onShopHealClip = null;
    [System.Serializable]
    struct CardsOfLevel
    {
        public List<Card> LevelCardPool;
        [Range(0,1f)]
        public float chanceForCardLevelBasic;
        [Range(0,1f)]
        public float chanceForCardLevelUpgraded;
    }

    [SerializeField]
    Image Heart = null;
    [SerializeField]
    List<CardsOfLevel> shopCardPool = null;
    [SerializeField]
    List<CardSlotShop> slots = null;
    List<Card> alreadyInShop = new List<Card>();
    [HideInInspector]
    bool healToFull = true;
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
    public void BoughtCard()
    {
        healToFull = false;
        Heart.enabled = healToFull;
    }
    public void OnShopEnter(bool isUpgraded = false)
    {
        healToFull = isUpgraded;
        Heart.enabled = healToFull;
        cardCost = Mathf.RoundToInt(basicCardCost * MapMovement.instance.roomMultiplier);

        alreadyInShop.Clear();
        foreach(CardSlotShop slot in slots)
        {
            Card tempCard;
            do
            {
                tempCard = GetCard(isUpgraded);
            } while (alreadyInShop.Contains(tempCard));
            alreadyInShop.Add(tempCard);
            slot.SetCard(tempCard);
            slot.SetDisplay(true);
        }
    }
    Card GetCard(bool isUpgraded)
    {
        float roll100 = Random.value;
        CardsOfLevel cardsOfLevelToGet = shopCardPool[0];
        foreach (CardsOfLevel cardsOfLevel in shopCardPool)
        {
            roll100 -= (isUpgraded) ? cardsOfLevel.chanceForCardLevelUpgraded : cardsOfLevel.chanceForCardLevelBasic;
            if (roll100 <= 0f)
            {
                cardsOfLevelToGet = cardsOfLevel;
                break;
            }
        }
        return cardsOfLevelToGet.LevelCardPool[Random.Range(0, cardsOfLevelToGet.LevelCardPool.Count)];
    }
    public void ExitShop()
    {
        if (healToFull)
        {
            PlayerInformation.instance.playerHealthAndEffects.ChangeHealthToMax();
            AudioController.instance.PlayEffect(onShopHealClip);
        }
        SceneController.instance.ChangeScene(SceneController.SceneState.Map);
    }
    public void OnPointerEnter()
    {
        AudioController.instance.PlayEffect(onArrowPointerEnterClip);
    }
    public void OnPointerExit()
    {
        AudioController.instance.PlayEffect(onArrowPointerEnterClip);
    }
}
