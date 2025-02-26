using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class HPBoost : AbilityBase
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
            player.MaxHpBuf -= abilityData.values[0];
        }

        // ���ο� HP ���� ����
        float previousMaxHp = player.MaxHp;  // ���� �ִ� ü�� ����
        float hpBoost = isUpgraded ? abilityData.values[1] : abilityData.values[0] * 0.01f;
        player.MaxHpBuf += hpBoost;
        float newMaxHp = player.MaxHp;  // ���ο� �ִ� ü�� ����

        // ������ ü�¸�ŭ ȸ��
        float hpIncrease = newMaxHp - previousMaxHp;
        player.ChangeHealth(hpIncrease);
        Debug.Log($"hp �ν�Ʈ {player.MaxHpBuf} ü�� ����");
    }
}