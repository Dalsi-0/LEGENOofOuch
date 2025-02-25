using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange = 3;

    [Header("")]
    [SerializeField] MonsterEnum mEnum;
    [SerializeField] GameObject potionPrefeb;
    [SerializeField][Range(0, 100)] float potionDrop;
    MonsterManager MM;

    void Start()
    {
        target = FindAnyObjectByType(typeof(PlayerCharacter)).GameObject().transform;
        MM = FindFirstObjectByType<MonsterManager>();
    }

    /// <summary>
    /// 공격사거리보다 플레이어가 멀리 있으면 플레이어 쪽으로 이동합니다. 아니면 그자리에서 공격하도록 합니다.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        moveDir = Vector2.zero;
        if (target == null)
            return;

        lookDir = (target.transform.position - transform.position).normalized;
        if (TargetDis > attackRange)
            moveDir = lookDir;
    }

    /// <summary>
    /// 적은 사망시 확률적으로 포션을 하나 드랍하고, 남은 적 리스트에서 제외됩니다.
    /// </summary>
    protected override void Death()
    {
        if (Random.Range(0, 100) < potionDrop)
            Instantiate(potionPrefeb, transform.position, Quaternion.identity);

        MM.RemoveEnemyOnDeath(this);
        base.Death();
    }
}
