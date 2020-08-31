using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Status ", menuName = "Card Effect/Multiply Damage")]
public class MultiplyStatus : Effect, IGotStatus
{
    [SerializeField]
    int multiplier = 2;
    [SerializeField]
    Status status;
    [SerializeField]
    bool multiplyOnSelf = true;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (multiplyOnSelf)
            caster.MultiplyStatus(status, multiplier);
        else
            casterOpponent.MultiplyStatus(status, multiplier);

    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "multiplier": return new KeyValuePair<string, int>(multiplier.ToString(), 0);
            case "status": return new KeyValuePair<string, int>(status.name.ToString(), 2);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
    public Status GetStatus()
    {
        return status;
    }
    public int GetDuration(HealthAndStatuses caster)
    {
        return 0;
    }
}
