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
    private int attackCount = 0;  // ���� Ƚ�� ����

    protected override void Attack()
    {
        attackCount++; // ���� Ƚ�� ����

        if (attackCount >= attackThreshold)
        {
            animHandle.Attack(AttackSpeed);
            FireInAllDirections(); // ������ ���� ����
            attackCount = 0; // ī��Ʈ �ʱ�ȭ
            return;
        }

        base.Attack();
    }

    private void FireInAllDirections()
    {
        for (float angle = 0; angle < 360; angle += 30)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameManager.Instance.ProjectileManager.ShootEnemyProjectile(this.transform.position, direction, AttackPower);
        }
    }
}
