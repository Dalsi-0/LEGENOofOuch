using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AbilityAttackBoost : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
    }

    public override void UseSkill()
    {
        Debug.Log($"UseSkill{this.name}");
    }
}