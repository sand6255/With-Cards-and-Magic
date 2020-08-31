using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Add Armor Per Status Duration ", menuName = "Card Effect/AddArmorPerStatusDuration")]
public class AddArmorPerStatusDuration : Effect
{
    int armorPerStack = 1;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
     
        caster.GiveArmor(armorPerStack * power);
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "damagePerStack": return new KeyValuePair<string, int>(armorPerStack.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }

}