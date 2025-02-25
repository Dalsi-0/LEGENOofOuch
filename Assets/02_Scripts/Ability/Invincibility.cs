using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Invincibility : AbilityBase
{
    [SerializeField] private Material invincibleMat;
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
            Material originalMat = player.GetComponent<SpriteRenderer>().material; // ���� ��Ƽ���� ����

            player.GodMod = true; // ���� ���� Ȱ��ȭ
            Debug.Log("���� ����");

            StartCoroutine(BlinkEffect(player, invincibleMat, originalMat)); // �����̴� ȿ��

            yield return new WaitForSeconds(2); // 2�� ���� ����

            player.GodMod = false; // ���� ���� ����
            player.GetComponent<SpriteRenderer>().material = originalMat; // ���� ��Ƽ���� ����
            Debug.Log("���� ����");

            yield return new WaitForSeconds(cooldownTime); // {n}�� ���
        }
    }

    private IEnumerator BlinkEffect(PlayerCharacter player, Material blinkMat, Material originalMat)
    {
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 6; i++) // 6�� �����̰� ����
        {
            sprite.material = (i % 2 == 0) ? blinkMat : originalMat;
            yield return new WaitForSeconds(0.2f); // 0.2�ʸ��� �����̱�
        }

        sprite.material = originalMat; // ���� ��Ƽ����� ����
    }
}