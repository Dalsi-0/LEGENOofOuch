using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CriticalMaster : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
    }
    protected override void UpdateAbility()
    {
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;

        if (isUpgraded)
        {
            player.CriChanceBuf -= abilityData.values[0];
        }
        float criChanceBoost = isUpgraded ? abilityData.values[1] : abilityData.values[0] * 0.01f;
        player.CriChanceBuf += criChanceBoost;
        player.CriDmgBuf = 0.4f;

        Debug.Log($"ũ��Ƽ�� ������ {player.AtkBuf} Ȯ�� ���� �� ũ�� ���� ���� {player.CriDmgBuf}");
    }
}