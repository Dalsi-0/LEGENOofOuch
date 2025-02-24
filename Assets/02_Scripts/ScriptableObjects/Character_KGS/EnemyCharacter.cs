using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange;

    [Header("")]
    [SerializeField] MonsterEnum mEnum;
    [SerializeField] GameObject potionPrefeb;
    [SerializeField][Range(0, 100)] float potionDrop;
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

    /// <summary>
    /// ���� ����� Ȯ�������� ������ �ϳ� ����ϰ�, ���� �� ����Ʈ���� ���ܵ˴ϴ�.
    /// </summary>
    protected override void Death()
    {
        if (Random.Range(0, 100) < potionDrop)
            Instantiate(potionPrefeb, new Vector3(transform.position.x,transform.position.y), Quaternion.identity);

            MM.RemoveEnemyOnDeath(this);
        base.Death();
    }
}
