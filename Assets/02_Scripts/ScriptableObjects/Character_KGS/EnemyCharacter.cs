using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange;

    [Header("")]
    [SerializeField] MonsterEnum mEnum;
    MonsterManager MM;

    void Start()
    {
        target = FindAnyObjectByType(typeof(PlayerCharacter)).GameObject().transform;
    }

    /// <summary>
    /// ���ݻ�Ÿ����� �÷��̾ �ָ� ������ �÷��̾� ������ �̵��մϴ�. �ƴϸ� ���ڸ����� �����ϵ��� �մϴ�.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        lookDir = (target.transform.position - transform.position).normalized;
        if (TargetDis > attackRange)
            moveDir = lookDir;
        else
            moveDir = Vector2.zero;
    }

    protected override void Death()
    {
        MM.RemoveEnemyOnDeath(this);
        base.Death();
    }
}
