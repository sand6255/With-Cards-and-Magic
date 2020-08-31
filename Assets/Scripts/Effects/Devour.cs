using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Devour ", menuName = "Card Effect/Devour")]
public class Devour : Effect,IDamage
{
    [SerializeField]
    int damage = 5, additionalHealth = 5;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        casterOpponent.TakeDamage(damage, caster);
        if (casterOpponent.currentHealth <= 0)
            caster.IncreaseMaximumHealth(additionalHealth);
    }
    public int GetDamage(HealthAndStatuses caster)
    {
        return Mathf.RoundToInt((damage + caster.attackPower) * caster.attackPowerCoef);
    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "damage": return GetDamagePair(caster, damage);
            case "additionalHealth": return new KeyValuePair<string, int>(additionalHealth.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }

}