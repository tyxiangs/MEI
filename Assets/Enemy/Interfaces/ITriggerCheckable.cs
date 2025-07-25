using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsWithinAttackingDistance { get; set; }
    void SetAggroStatus(bool isAggroed);
    void SetAttackingDistanceBool(bool isWithinAttackingDistance);
}
