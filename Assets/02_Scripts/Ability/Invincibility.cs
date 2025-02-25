using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Invincibility : AbilityBase
{
    private float cooldownTime;
    private Coroutine invincibilityCoroutine; // �ڷ�ƾ ���� ����

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;
        // ���� �ڷ�ƾ�� �ִٸ� ����
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }
        invincibilityCoroutine = StartCoroutine(ActivateInvincibility(player));
    }

    protected override void UpdateAbility()
    {
        cooldownTime = abilityData.values[isUpgraded ? 1 : 0];
    }

    /// <summary>
    /// 2�� ���� ���� ���� ���� ��, n�� �� �ٽ� ����
    /// </summary>
    private IEnumerator ActivateInvincibility(PlayerCharacter player)
    {
        while (player != null && player.GetCurHp() > 0)
        {
            player.GodMod = true; // ���� ���� Ȱ��ȭ
            Debug.Log("���� ����");

            yield return new WaitForSeconds(2); // 2�� ���� ����

            player.GodMod = false; // ���� ���� ����
            Debug.Log("���� ����");

            yield return new WaitForSeconds(cooldownTime); // {n}�� ���
        }
    }
}