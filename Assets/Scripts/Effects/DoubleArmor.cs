using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Armor ", menuName = "Card Effect/DoubleArmor")]
public class DoubleArmor : Effect
{
    [SerializeField]
    int multiplier = 2;
    [SerializeField]
    bool armorToCaster = true;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (armorToCaster)
            caster.SetArmor(caster.armor * 2);
        else
            casterOpponent.SetArmor(casterOpponent.armor * 2);

    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "multiplier": return new KeyValuePair<string, int> (multiplier.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}