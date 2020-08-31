using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable,CreateAssetMenu(fileName = "New Card ", menuName = "Card")]
public class Card : ScriptableObject
{
    
    public const string increaseColorText = "<color=green>";
    public const string decreaseColorText = "<color=red>";
    public const string statusText = "<b>";
    public const string endChangeText = "</b></color>";
    [SerializeField]
    List<Effect> applyEffectsOnCardPlay;
    public string nameText,cardText;
    public int manaCost;
    public Sprite cardSprite;
    public Card CreateCardCopy()
    {
        Card newCard = new Card();
        newCard.nameText = nameText;
        newCard.cardText = cardText;
        newCard.manaCost = manaCost;
        newCard.applyEffectsOnCardPlay = applyEffectsOnCardPlay;
        newCard.cardSprite = cardSprite;
        return newCard;
}
    public void onPlay(HealthAndStatuses caster)
    {
        foreach (Effect effect in applyEffectsOnCardPlay)
        {
            effect.OnEffectPlay(PlayerInformation.instance.playerHealthAndEffects,EnemyFight.instance.enemyHealthAndStatus);
        }
        //if(Random.Range(0,2)==1) BattleController.instance.DrawCards(1);
    }
    public string GetCardText(HealthAndStatuses caster, StatusDisplayDetailsSpawner statusDisplayDetailsSpawner = null)
    {
        string transformedCardText = "";
        bool errorOccurred = false;
        for(int i = 0; i < cardText.Length - 1; i++)
        {
            if (cardText[i] == '/' && cardText[i + 1] == '*')
            {
                i += 2;
                bool gotIndex = false;
                bool indexGotNumbers = false;
                int indexToGetValue = 0;
                string textToGetValue = "";
                while (i < cardText.Length - 1 && (cardText[i] != '*' || cardText[i + 1] != '/'))
                {
                    if (!gotIndex && cardText[i] >= '0' && cardText[i] <= '9') 
                    {
                        indexToGetValue = indexToGetValue * 10 + cardText[i] - '0';
                        indexGotNumbers = true;
                    }
                    else
                    {
                        textToGetValue += cardText[i];
                        gotIndex = true;
                    }
                    
                    i++;
                }
                if (gotIndex == false || indexGotNumbers == false || indexToGetValue >= applyEffectsOnCardPlay.Count)
                    Debug.LogError("Card " + name + " got wrong index in cardText");
                else if(i >= cardText.Length)
                    Debug.LogError("Didn't find */");
                else
                {
                    KeyValuePair<string,int> resultPair = applyEffectsOnCardPlay[indexToGetValue].GetNumberByString(textToGetValue, caster);

                    switch(resultPair.Value)
                    {
                        case -1:
                            transformedCardText += decreaseColorText;
                            break;
                        case 0:
                            break;
                        case 1:
                            transformedCardText += increaseColorText;
                            break;
                        case 2:
                            transformedCardText += statusText;
                            if (statusDisplayDetailsSpawner != null)
                                statusDisplayDetailsSpawner.AddStatus((applyEffectsOnCardPlay[indexToGetValue] as IGotStatus).GetStatus(), (applyEffectsOnCardPlay[indexToGetValue] as IGotStatus).GetDuration(PlayerInformation.instance.playerHealthAndEffects));
                            break;
                        default:
                            errorOccurred = true;
                            break;
                    }
                    if(errorOccurred)
                        Debug.LogError("Card " + name + " got wrong string to convert");
                    else
                        transformedCardText += resultPair.Key;
                    transformedCardText += endChangeText;
                }
                i++; // skip last '/'
            }
            else
                transformedCardText += cardText[i];
        }
        if(cardText[cardText.Length - 1] != '/')
            transformedCardText += cardText[cardText.Length - 1];
        return transformedCardText;
    }
}
