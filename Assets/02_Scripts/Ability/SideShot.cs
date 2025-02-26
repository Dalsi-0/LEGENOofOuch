using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SideShot : AbilityBase
{
    private GameManager gameManager;
    private ProjectileManager projectileManager;
    private PlayerCharacter player;
    private float offset = 0.5f; // ȭ�� ����
    int arrowCount;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;
        player = gameManager.player;
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // ȭ�� ���� ���� (���׷��̵� ��: �¿� 1����, ���׷��̵� ��: �¿� 2����)
        arrowCount = isUpgraded ? 2 : 1;
    }

    public override void UseSkill()
    {
        PlayerClassEnum pClass = player.GetPlayerClass();

        // ĳ���Ͱ� �ٶ󺸴� ���� ����
        Vector3 lookDir = player.GetlookDir().normalized;

        // lookDir �������� ������ ���� ���� ���ϱ� (���� ���� �̿�)
        Vector3 rightDir = Vector3.Cross(Vector3.forward, lookDir).normalized;
        Vector3 leftDir = -rightDir;

        for (int i = 0; i < arrowCount; i++)
        {
            // ��Ī�� ��ġ ���
            float posOffset = (i - (arrowCount - 1) / 2.0f) * offset;
            Vector3 leftSpawnPos = player.transform.position + rightDir * posOffset;
            Vector3 rightSpawnPos = player.transform.position - rightDir * posOffset;

            projectileManager.ShootPlayerProjectile(leftSpawnPos, leftDir, pClass);
            projectileManager.ShootPlayerProjectile(rightSpawnPos, rightDir, pClass);
        }
    }
}