using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gain Status Per Status Duration ", menuName = "Card Effect/GainStatusPerStatusDuration")]
public class GainStatusPerStatusDuration : Effect
{
    [SerializeField]
    Status statusToGain;

    int statusPerStack = 1;
    [SerializeField]
    bool applyOnSelf = true;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (applyOnSelf)
            caster.TakeStatus(statusToGain, power);
        else
            casterOpponent.TakeStatus(statusToGain, power);
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "statusPerStack": return new KeyValuePair<string, int>(statusPerStack.ToString(), 0);
            case "status": return new KeyValuePair<string, int>(statusToGain.name, 2);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }

}
