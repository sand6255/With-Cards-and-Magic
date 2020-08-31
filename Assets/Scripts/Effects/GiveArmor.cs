using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GiveArmor ", menuName = "Card Effect/GiveArmor")]
public class GiveArmor : Effect
{
    [SerializeField]
    int armor = 5;
    [SerializeField]
    bool armorToCaster = true;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if(armorToCaster)
            caster.GiveArmor(armor, caster);
        else
            casterOpponent.GiveArmor(armor, caster);
        
    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "armor": return GetArmorPair(caster, armor);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
