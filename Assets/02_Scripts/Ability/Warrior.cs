using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Warrior : AbilityBase
{
    private float skillCooldown = 5f; // 5�ʸ��� ����
    private float sizeMultiplier; // {0}% ���� ��
    private Coroutine skillCoroutine; // ��ų ���� �ڷ�ƾ

    private GameManager gameManager;
    private ProjectileManager projectileManager;
    private PlayerCharacter player;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;
        player = gameManager.player;

        UpdateAbility();

        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
        }
        skillCoroutine = StartCoroutine(AutoShootProjectile());
    }

    protected override void UpdateAbility()
    {
        sizeMultiplier = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        sizeMultiplier *= 0.01f;
    }

    private IEnumerator AutoShootProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(skillCooldown);

            Vector3 playerPos = player.transform.position;
            Vector3 lookDir = player.GetlookDir();
            PlayerClassEnum pClass = player.GetPlayerClass();

            Vector3 projectileScale = Vector3.one * sizeMultiplier; // ũ�� ����

            // �Ŵ��� �˱� �߻�
            GameObject obj = projectileManager.ShootBigSwordAura(playerPos, lookDir, pClass);
            obj.transform.localScale = projectileScale;
        }
    }
}