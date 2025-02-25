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

        // ������ ���� ���� �ڷ�ƾ�� �ִٸ� ����
        if (furyCoroutine != null)
        {
            StopCoroutine(furyCoroutine);
        }
        furyCoroutine = StartCoroutine(UpdateFuryDamage(player));
    }

    protected override void UpdateAbility()
    {
        damageIncreasePerPercent = abilityData.values[isUpgraded ? 1 : 0] * 0.01f;
    }

    private IEnumerator UpdateFuryDamage(PlayerCharacter player)
    {
        while (player != null && player.GetCurHp() > 0)
        {
            // ���� ü�¿� ���� �߰� ���ݷ� ���
            float lostHpPercent = (1 - (player.GetCurHp() / player.MaxHp)) * 100;
            float newFuryAtkBonus = lostHpPercent * damageIncreasePerPercent;

            // ���� Fury ���ʽ��� ���� �� �� ���� �߰�
            player.AtkBuf -= furyAtkBonus;
            furyAtkBonus = newFuryAtkBonus;
            player.AtkBuf += furyAtkBonus;

            Debug.Log($" ���� ü��: {lostHpPercent}%, Fury �߰� ���ݷ�: {furyAtkBonus * 100}%");

            yield return new WaitForSeconds(0.5f); // 0.5�ʸ��� ������Ʈ
        }
    }
}