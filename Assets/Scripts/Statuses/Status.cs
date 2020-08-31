using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new status ", menuName = "status")][System.Serializable]
public class Status : ScriptableObject
{
    [SerializeField]
    List<Effect> onStatusStart = null, onStatusEnd = null, onStartOfTurn = null, onEndOfTurn = null, onCardPlay = null, onTakeDamage = null, onDurationChange = null;
    public Sprite sprite = null;
    public new string name = "";
    public string statusText = "";
    public bool TickOnStartOfTurn = false, TickOnEndOfTurn = false, TickOnCardPlay = false, TickOnTakeDamage = false;
    public bool canBeNegative = false, oneTurnOnly = false;
    [SerializeField]
    string positiveText = "", negativeText = "";

    public const string increaseColorText = "<color=green>";
    public const string decreaseColorText = "<color=red>";
    public const string endChangeText = "</b></color>";

    public void OnStatusStart(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onStatusStart)
            effect.OnEffectPlay(statusOn, opponent, duration);
    }
    public void OnTakeDamage(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onTakeDamage)
            effect.OnEffectPlay(statusOn, opponent, duration);
    }
    public void OnStatusEnd(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onStatusEnd)
            effect.OnEffectPlay(statusOn, opponent, duration);
    }
    public void OnEndOfTurnEvent(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onEndOfTurn)
            effect.OnEffectPlay(statusOn, opponent, duration);
    }
    public void OnStartOfTurnEvent(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onStartOfTurn)
            effect.OnEffectPlay(statusOn, opponent, duration);
    }
    public void OnCardPlayEvent(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onCardPlay)
            effect.OnEffectPlay(statusOn, opponent, duration);
    }
    public void OnDurationChangeEvent(HealthAndStatuses statusOn, HealthAndStatuses opponent, int duration)
    {
        foreach (Effect effect in onDurationChange)
            effect.OnEffectPlay(statusOn, opponent, duration);    
    }
    public string GetStatusText(int duration)
    {
        string transformedStatusText = "";
        for (int i = 0; i < statusText.Length - 1; i++)
        {
            if (statusText[i] == '/' && statusText[i + 1] == '*')
            {
                i += 2;
                string textToGetValue = "";
                while (i < statusText.Length - 1 && (statusText[i] != '*' || statusText[i + 1] != '/'))
                {
                    textToGetValue += statusText[i];


                    i++;
                }
                switch (textToGetValue)
                {
                    case "number":
                        {
                            if (duration > 0)
                                transformedStatusText += increaseColorText + duration.ToString();
                            else if (duration < 0)
                                transformedStatusText += decreaseColorText + Mathf.Abs(duration).ToString();
                            else
                                transformedStatusText += "X";
                            transformedStatusText += endChangeText;
                        }

                        break;
                    case "text":
                        if (duration >= 0)
                            transformedStatusText += positiveText;
                        else if(duration < 0)
                            transformedStatusText += negativeText;
                        break;
                    case "s":
                        transformedStatusText += 's';
                        break;
                    default:
                        Debug.LogError("can't replace " + transformedStatusText + " at " + name);
                        break;

                }
                i++; // skip last '/'
            }
            else
                transformedStatusText += statusText[i];
        }
        if (statusText[statusText.Length - 1] != '/')
            transformedStatusText += statusText[statusText.Length - 1];
        return transformedStatusText;
    }
}
