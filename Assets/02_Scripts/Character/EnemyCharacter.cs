using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange = 3;

    [Header("")]
    [SerializeField] MonsterEnum mEnum;
    [SerializeField] GameObject potionPrefeb;
    [SerializeField][Range(0, 100)] float potionDrop;

    protected override bool IsAttacking => base.IsAttacking && TargetDis <= attackRange;

    NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        agent.speed = Speed;
    }

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
        if (target == null)
            return;

        Vector2 deltaP = agent.nextPosition - transform.position;
        if (TargetDis > attackRange && deltaP.magnitude < attackRange*2)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);
        moveDir = (agent.nextPosition - transform.position).normalized;
    }

    /// <summary>
    /// ���� ����� Ȯ�������� ������ �ϳ� ����ϰ�, ���� �� ����Ʈ���� ���ܵ˴ϴ�.
    /// </summary>
    protected override void Death()
    {
        if (Random.Range(0, 100) < potionDrop)
            Instantiate(potionPrefeb, transform.position, Quaternion.identity);

        GameManager.Instance.KillMonster(this);
        base.Death();
    }

    protected override void Attack()
    {
        base.Attack();
        GameManager.Instance.ProjectileManager.ShootEnemyProjectile(transform.position, LookDir, AttackPower);
    }
}
