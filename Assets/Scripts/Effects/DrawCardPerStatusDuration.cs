using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Draw Card Per Status Duration ", menuName = "Card Effect/DrawCardPerStatusDuration")]
public class DrawCardPerStatusDuration : Effect
{
    int cardsPerStack = 1;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        Debug.Log("CardDrawChange = " + power);
        BattleController.instance.StartOfTurnCardChange(power * cardsPerStack);
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "damagePerStack": return new KeyValuePair<string, int>(cardsPerStack.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
