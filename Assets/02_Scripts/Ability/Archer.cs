using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Archer : AbilityBase
{
    private int attackCount = 0;  // ���� Ƚ�� ����
    private int attackThreshold;  // {0}ȸ���� �߻��� ����
    private float[] fireAngles = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 }; // ������ ����

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
    }

    protected override void UpdateAbility()
    {
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        attackThreshold = (int)value; // {0}ȸ���� ������ �߻�
    }

    public override void UseSkill()
    {
        attackCount++; // ���� Ƚ�� ����

        if (attackCount >= attackThreshold)
        {
            FireInAllDirections(); // ������ ���� ����
            attackCount = 0; // ī��Ʈ �ʱ�ȭ
        }
    }

    private void FireInAllDirections()
    {
        Vector3 playerPos = player.transform.position;
        PlayerClassEnum pClass = player.GetPlayerClass();
       /* int wallCount = projectileManager.GetWallCount();
        int contactCount = projectileManager.GetContactCount();

        foreach (float angle in fireAngles)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            projectileManager.ShootPlayerProjectile(playerPos, direction, pClass, wallCount, contactCount);
        }*/
    }
}