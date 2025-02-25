using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage = 0.2f, criticalChance;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;

    /// <summary>
    /// Ű���� �Է����� �̵������� �����մϴ�.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        SearchTarget();
    }

    /// <summary>
    /// ���� Ȱ��ȭ�� ���� �� ���� ����� ���� ��ǥ�� ����ϴ�.
    /// </summary>
    void SearchTarget()
    {
        target = null;
        var enemys = FindObjectsOfType(typeof(EnemyCharacter));
        foreach (var enemy in enemys)
        {
            float distance = (enemy.GameObject().transform.position - transform.position).magnitude;

            if (distance < TargetDis)
                target = enemy.GameObject().transform;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        GameManager.Instance.ProjectileManager.ShootPlayerProjectile(GameManager.Instance.player.transform.position, lookDir, pClass, 0, 0);
    }

    protected override void Death()
    {
        //����� �������� ���� ����
        base.Death();
    }
}
