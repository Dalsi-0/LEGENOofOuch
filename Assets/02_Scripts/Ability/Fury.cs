using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Fury : AbilityBase
{
    private float damageIncreasePerPercent; // ���� ü�� 1%�� ���ݷ� ������
    private float furyAtkBonus = 0f; // Fury ��ų�� �����ϴ� �߰� ���ݷ�
    private Coroutine furyCoroutine; // �ڷ�ƾ ����� ����

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;

        UpdateAbility();
        // ������ ���� ���� �ڷ�ƾ�� �ִٸ� ����
        if (furyCoroutine != null)
        {
            StopCoroutine(furyCoroutine);
        }
        furyCoroutine = StartCoroutine(UpdateFuryDamage(player));
    }

    protected override void UpdateAbility()
    {
        damageIncreasePerPercent = (isUpgraded ? abilityData.values[1] : abilityData.values[0]) * 0.01f;
    }

    private IEnumerator UpdateFuryDamage(PlayerCharacter player)
    {
        while (player != null && player.CurHp > 0)
        {
            // ���� ü�¿� ���� �߰� ���ݷ� ���
            float lostHpPercent = (1 - (player.CurHp / player.MaxHp)) * 100;
            float newFuryAtkBonus = (int)lostHpPercent * damageIncreasePerPercent;

            // ���� Fury ���ʽ��� ���� �� �� ���� �߰�
            player.AtkBuf -= furyAtkBonus;
            furyAtkBonus = newFuryAtkBonus;
            player.AtkBuf += furyAtkBonus;

            yield return new WaitForSeconds(0.5f);
        }
    }
}