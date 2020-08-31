using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGotStatus
{
    Status GetStatus();
    int GetDuration(HealthAndStatuses caster);
}
