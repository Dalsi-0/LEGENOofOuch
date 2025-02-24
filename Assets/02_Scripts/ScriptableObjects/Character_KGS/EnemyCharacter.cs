using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange;

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
}
