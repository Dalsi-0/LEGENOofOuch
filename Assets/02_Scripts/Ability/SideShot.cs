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

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;
        player = gameManager.player;
    }

    public override void UseSkill()
    {
        PlayerClassEnum pClass = player.GetPlayerClass();
       /* int wallCount = projectileManager.GetWallCount();
        int contactCount = projectileManager.GetContactCount();

        // ȭ�� ���� ���� (���׷��̵� ��: �¿� 1����, ���׷��̵� ��: �¿� 2����)
        int arrowCount = isUpgraded ? 2 : 1;

        // 90�� ���� ���� (�¿� ����)
        Vector3 leftDir = Quaternion.Euler(0, 0, 90) * Vector3.right;
        Vector3 rightDir = Quaternion.Euler(0, 0, -90) * Vector3.right;

        for (int i = 0; i < arrowCount; i++)
        {
            // ��Ī�� ��ġ ���
            float posOffset = (i - (arrowCount - 1) / 2.0f) * offset;
            Vector3 leftSpawnPos = player.transform.position + Vector3.up * posOffset;
            Vector3 rightSpawnPos = player.transform.position + Vector3.up * posOffset;

            projectileManager.ShootPlayerProjectile(leftSpawnPos, leftDir, pClass, wallCount, contactCount);
            projectileManager.ShootPlayerProjectile(rightSpawnPos, rightDir, pClass, wallCount, contactCount);
        }*/
    }
}