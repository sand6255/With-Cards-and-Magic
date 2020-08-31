using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    int GetDamage(HealthAndStatuses caster);
}
