using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Effect : ScriptableObject
{
    abstract public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0);
    abstract public KeyValuePair<string,int> GetNumberByString(string numberName, HealthAndStatuses caster);
    protected KeyValuePair<string, int> GetDamagePair(HealthAndStatuses caster, int startingDamage, int additionalDamage = 0) 
    {
        int code;
        int damage;
        damage = Mathf.RoundToInt((startingDamage + additionalDamage + caster.attackPower) * caster.attackPowerCoef);
        if (damage < 0)
            damage = 0;
        if (damage < startingDamage)
            code = -1;
        else if (damage == startingDamage)
            code = 0;
        else
            code = 1;
        return new KeyValuePair<string, int>(damage.ToString(), code);
    }
    protected KeyValuePair<string, int> GetArmorPair(HealthAndStatuses caster, int startingArmor, int additionalArmor = 0)
    {
        int code;
        int armor;
        armor = Mathf.RoundToInt((startingArmor + additionalArmor + caster.armorPower) * caster.armorPowerCoef);
        if (armor < 0)
            armor = 0;
        if (armor < startingArmor)
            code = -1;
        else if (armor == startingArmor)
            code = 0;
        else
            code = 1;
        return new KeyValuePair<string, int>(armor.ToString(), code);
    }
}
