using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Invincibility : AbilityBase
{
    [SerializeField] private Material invincibleMat;
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
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
        spriteRenderer = player.GetPlayerSpriteRenderer();
        originalMat = player.GetPlayerSpriteRenderer().material;
        invincibilityCoroutine = StartCoroutine(ActivateInvincibility(player));
    }

    protected override void UpdateAbility()
    {
        cooldownTime = isUpgraded ? abilityData.values[1] : abilityData.values[0];
    }

    /// <summary>
    /// 2�� ���� ���� ���� ���� ��, n�� �� �ٽ� ����
    /// </summary>
    private IEnumerator ActivateInvincibility(PlayerCharacter player)
    {
        while (player != null && player.CurHp > 0)
        {
            player.GodMod = true; // ���� ���� Ȱ��ȭ

            StartCoroutine(BlinkEffect(player, invincibleMat, originalMat)); // �����̴� ȿ��

            yield return new WaitForSeconds(2); // 2�� ���� ����

            player.GodMod = false; // ���� ���� ����
            spriteRenderer.material = originalMat; // ���� ��Ƽ���� ����

            yield return new WaitForSeconds(cooldownTime); // {n}�� ���
        }
    }

    private IEnumerator BlinkEffect(PlayerCharacter player, Material blinkMat, Material originalMat)
    {
        for (int i = 0; i < 6; i++) // 6�� �����̰� ����
        {
            spriteRenderer.material = (i % 2 == 0) ? blinkMat : originalMat;
            yield return new WaitForSeconds(0.15f); // 0.15�ʸ��� �����̱�
        }

        spriteRenderer.material = originalMat; // ���� ��Ƽ����� ����
    }
}