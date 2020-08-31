using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor Attack ", menuName = "Card Effect/ArmorAttack")]
public class ArmorAttack : Effect, IDamage
{
    [SerializeField]
    int damagePerArmor = 1;
    [SerializeField]
    bool dealDamageToCaster = false;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (dealDamageToCaster)
            caster.TakeDamage(caster.armor * damagePerArmor, caster);
        else
            casterOpponent.TakeDamage(caster.armor * damagePerArmor, caster);
    }
    public int GetDamage(HealthAndStatuses caster)
    {
        if (!dealDamageToCaster)
            return Mathf.RoundToInt((caster.armor * damagePerArmor + caster.attackPower) * caster.attackPowerCoef);
        else
            return 0;
    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "damage": return GetDamagePair(caster,0,damagePerArmor*caster.armor);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }

}
