using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Damage ", menuName = "Card Effect/DealDamage")]
public class DealDamage : Effect, IDamage
{
    [SerializeField]
    int damage = 5;
    [SerializeField]
    bool dealDamageToCaster = false;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (dealDamageToCaster)
            caster.TakeDamage(damage, caster);
        else
            casterOpponent.TakeDamage(damage, caster);
    }
    public int GetDamage(HealthAndStatuses caster)
    {
        if (!dealDamageToCaster)
            return Mathf.RoundToInt((damage + caster.attackPower) * caster.attackPowerCoef);
        else 
            return 0;
    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "damage": return GetDamagePair(caster, damage);
            default: return new KeyValuePair<string, int>(0.ToString(),-2);
        }
    }
    
}
