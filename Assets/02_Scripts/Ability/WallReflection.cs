using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WallReflection : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        // �ݻ� Ƚ�� ī��Ʈ ����
      //  GameManager.Instance.ProjectileManager.SetWallReflectCount((int)value);

        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        
        // ���ط� ����
      //  GameManager.Instance.ProjectileManager.SetWallReflectDamage((int)value);
    }
}