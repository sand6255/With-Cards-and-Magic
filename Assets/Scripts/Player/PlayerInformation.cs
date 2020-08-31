using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class PlayerInformation : MonoBehaviour
{
    public HealthAndStatusesPlayer playerHealthAndEffects;
    [SerializeField]
    List<Card> deckOfCardsOnStart = new List<Card>();
    List<Card> deckOfCards;
    public static PlayerInformation instance = null;
    int windShards = 0;
    [SerializeField]
    int startingHealth = 0;
    [SerializeField]
    TextMeshProUGUI healthTextTopPanel = null, healthTextBattlePanel = null, windText = null;
    [SerializeField]
    int windShardsStartingValue = 100;
    [SerializeField]
    StatusDisplayIconSpawner statusDisplayer = null;
    public int maximumManaOnStart = 5;
    [SerializeField]
    CustomAnimation customAnimation;
    [SerializeField]
    AudioClip takeDamageClip;
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
        playerHealthAndEffects = new HealthAndStatusesPlayer();
        playerHealthAndEffects.SetStatusDisplayer(statusDisplayer);
        InitializeManager();
    }
    public void InitializeManager()
    {
        playerHealthAndEffects.SetComponent(startingHealth, healthTextTopPanel);
        if(deckOfCards != null)deckOfCards.Clear();
        deckOfCards = new List<Card>(deckOfCardsOnStart);
        SetWindShards(windShardsStartingValue);
    }
    public List<Card> GetDeck()
    {
        return deckOfCards;
    }

    public void AddWindShards(int number)
    {
        windShards += number;
        UpdateWindText();
    }
    public void SetWindShards(int number)
    {
        windShards = number;
        UpdateWindText();
    }
    private void UpdateWindText()
    {
        windText.text = windShards.ToString();
    }
    public void TookDamage()
    {
        AudioController.instance.PlayEffect(takeDamageClip);
        customAnimation.PlayTakeDamageAnimation();
    }
    public bool UseWindShards(int amount)
    {
        if (windShards >= amount)
        {
            windShards -= amount;
            UpdateWindText();
            return true;
        }
        else
            return false;
    }
    public void AddCardToDeck(Card card)
    {
        deckOfCards.Add(card);
    }
    private void Update()
    {
        healthTextBattlePanel.text = healthTextTopPanel.text;
    }

}
