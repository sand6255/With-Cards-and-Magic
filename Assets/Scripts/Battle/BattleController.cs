using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleController : MonoBehaviour
{
    public static BattleController instance = null;
    bool inBattle = false;
    bool playerTurn = false;
    List<Card> deck = new List<Card>();
    [SerializeField]
    int drawOnStart = 2;
    [SerializeField]
    int drawOnStartOfTurn = 2;
    [SerializeField]
    float cardDrawCooldown = 1f;
    [SerializeField]
    GameObject enemyMainObject = null;
    [SerializeField]
    TextMeshProUGUI manaText = null, deckText = null;
    int cardsToDraw = 0;
    public bool coroutineIsGoing = false;
    [HideInInspector]
    public int currentMana = 5, maximumMana = 5;
    int drawOnStartOfTurnChange = 0;
    [SerializeField]
    Card garbageCard;
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
    
    public void StartBattle(GameObject enemyObject, Enemy enemy, float stagePower = 1f)
    {    
        PlayerInformation.instance.playerHealthAndEffects.PlayerEndOrStartOfBattleReset();
        enemyObject = SpawnEnemyImage(enemyObject);
        deck = new List<Card>(PlayerInformation.instance.GetDeck());
        ShuffleDeck(deck);
        inBattle = true;
        SceneController.instance.ChangeScene(SceneController.SceneState.Battle);
        EnemyFight.instance.GenerateEnemyFight(enemyObject,enemy, stagePower);
        

        cardsToDraw = 0;
        StopAllCoroutines();
        coroutineIsGoing = false;
        BattleHandOfCards.instance.ClearHand();

        DrawCards(drawOnStart);
        SetMaxManaInitial();
        RefillMana();

        GiveTurnPlayer();
    }
    void SetMaxManaInitial()
    {
        maximumMana = PlayerInformation.instance.maximumManaOnStart;
        SetManaText();
    }
    public void IncreaseMaximumMana(int num)
    {
        maximumMana += num;
        if (maximumMana < 0)
            maximumMana = 0;
        SetManaText();

    }
    public void IncreaseCurrentMana(int num)
    {
        currentMana += num;
        if (currentMana < 0)
            currentMana = 0;
        SetManaText();

    }
    public bool IsPlayerTurn()
    {
        return playerTurn;
    }
    private void GiveTurnPlayer()
    {
        PlayerInformation.instance.playerHealthAndEffects.SetArmor(0);
        EnemyFight.instance.SetNextMove();
        DrawCards(drawOnStartOfTurn + drawOnStartOfTurnChange);
        drawOnStartOfTurnChange = 0;
        RefillMana();
        PlayerInformation.instance.playerHealthAndEffects.StartOfTurnStatusUpdate();
        playerTurn = true;
    }
    public void EndTurnPlayer()
    {
        PlayerInformation.instance.playerHealthAndEffects.EndOfTurnStatusUpdate();
        playerTurn = false;
        GiveTurnEnemy();
    }

    private void GiveTurnEnemy()
    {
        EnemyFight.instance.enemyHealthAndStatus.SetArmor(0);
        EnemyFight.instance.enemyHealthAndStatus.StartOfTurnStatusUpdate();
        EnemyFight.instance.TakeTurn();
    }
    public void EndTurnEnemy()
    {
        EnemyFight.instance.enemyHealthAndStatus.EndOfTurnStatusUpdate();
        GiveTurnPlayer();

    }
    private IEnumerator DrawCardsCoroutine()
    {
        coroutineIsGoing = true;
        
        while(cardsToDraw > 0 && coroutineIsGoing)
        {
            bool isEmpty = true;
            Card cardDraw;
            if (deck.Count > 0)
            {
                isEmpty = false;
                cardDraw = deck[0];
            }
            else
            {
                cardDraw = garbageCard;
            }
                if (BattleHandOfCards.instance.AddCard(cardDraw))
                {
                    if(!isEmpty)deck.Remove(cardDraw);
                    cardsToDraw--;
                    SetDeckText();
                    yield return new WaitForSeconds(cardDrawCooldown);
                }
                else
                {
                    cardsToDraw = 0;
                    break;
                }
        }
        coroutineIsGoing = false;
    }
    
    public void DrawCards(int numberOfCards)
    {
        if (numberOfCards <= 0)
            return;
        cardsToDraw += numberOfCards;
        if(!coroutineIsGoing)
             StartCoroutine(DrawCardsCoroutine());
    }
    public void StartOfTurnCardChange(int numberOfCards)
    {
        drawOnStartOfTurnChange += numberOfCards;
        Debug.Log(drawOnStartOfTurnChange);
    }
    private GameObject SpawnEnemyImage(GameObject enemy)
    {
        
        return Instantiate(enemy, enemyMainObject.transform, false);
    }
    public bool UseMana(int manaToUse)
    {
        if(currentMana >= manaToUse)
        {
            currentMana -= manaToUse;
            SetManaText();
            return true;
        }
        else
            return false;
    }
    public void RefillMana()
    {
        currentMana = maximumMana;
        SetManaText();
    }
    private void SetManaText()
    {
        manaText.text = currentMana + "/" + maximumMana;
    }
    private void SetDeckText()
    {
        deckText.text = deck.Count.ToString();
    }
    void ShuffleDeck(List<Card> deck)
    {
        if(deck != null)
            for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void EnemyDead()
    {
        playerTurn = false;
        inBattle = false;
        PlayerInformation.instance.playerHealthAndEffects.PlayerEndOrStartOfBattleReset();
        if (PlayerInformation.instance.playerHealthAndEffects.currentHealth <= 0)
            return;
        SceneController.instance.ChangeScene(SceneController.SceneState.Reward);
        ShopController.instance.OnShopEnter();
    }
    public bool PlayerCanPlayCard()
    {
        return playerTurn;
    }
}
