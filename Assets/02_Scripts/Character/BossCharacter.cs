using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossCharacter : EnemyCharacter
{
    [SerializeField] bool isBoss;
    [SerializeField] private int attackThreshold;  // {0}ȸ���� �߻��� ����
    private float[] fireAngles = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 }; // ������ ����
    private int attackCount = 0;  // ���� Ƚ�� ����

    protected override void Attack()
    {
        attackCount++; // ���� Ƚ�� ����

        if (attackCount >= attackThreshold)
        {
            FireInAllDirections(); // ������ ���� ����
            attackCount = 0; // ī��Ʈ �ʱ�ȭ
            return;
        }

        base.Attack();
    }

    private void FireInAllDirections()
    {
        foreach (float angle in fireAngles)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameManager.Instance.ProjectileManager.ShootEnemyProjectile(this.transform.position, direction, AttackPower);
        }
    }
}
