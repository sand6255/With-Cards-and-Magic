using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Delayed Damage ", menuName = "Card Effect/DealDelayedDamage")]
public class DealDamagePerStatusDuration : Effect
{
    int damagePerStack = 1;
    [SerializeField]
    bool dealDamageToStatusOwner = false;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (dealDamageToStatusOwner)
            caster.TakeDamage(power * damagePerStack);
        else
            casterOpponent.TakeDamage(power * damagePerStack);
    }
    
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "damagePerStack": return new KeyValuePair<string, int>(damagePerStack.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }

}