using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Change Armor Coef ", menuName = "Card Effect/ChangeArmorCoef")]
public class ChangeArmorCoef : Effect
{
    [SerializeField]
    float percentage = 0.5f;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        caster.armorPowerCoef *= percentage;
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
