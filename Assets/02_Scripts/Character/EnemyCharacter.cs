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

    NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
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
        agent.SetDestination(target.position);

        moveDir = TargetDis > attackRange ? (agent.nextPosition - transform.position).normalized : Vector2.zero;
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
        GameManager.Instance.ProjectileManager.ShootEnemyProjectile(this.transform.position, lookDir);
    }
}
