using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Draw Cards ", menuName = "Card Effect/DrawCards")]
public class DrawCards : Effect
{
    [SerializeField]
    int cardsToDraw = 1;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        BattleController.instance.DrawCards(cardsToDraw);
    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "cardsToDraw": return new KeyValuePair<string, int>(cardsToDraw.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
