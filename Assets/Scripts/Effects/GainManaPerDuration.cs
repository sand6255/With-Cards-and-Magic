using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gain Mana Per Status Duration ", menuName = "Card Effect/GainManaPerStatusDuration")]
public class GainManaPerDuration : Effect
{
    int manaPerStatusDuration = 1;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        BattleController.instance.IncreaseCurrentMana(power * manaPerStatusDuration);
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "manaPerStack": return new KeyValuePair<string, int>(manaPerStatusDuration.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
