using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    AnimationHandler animHandle;
    [SerializeField] protected Transform target;
    [SerializeField] Slider HpBar;

    [Header("Stat")]
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float attackPower = 1, attackSpeed = 1;

    float curHp;
    protected float CurHp { get => curHp; set => curHp = value <= maxHp ? (value >= 0 ? value : 0) : maxHp; }

    protected Rigidbody2D rig;
    protected Vector2 lookDir, moveDir;
    protected bool IsMove => moveDir.magnitude > 0.5f;
    protected float TargetDis => target == null ? float.MaxValue : (target.position - transform.position).magnitude;

    protected bool IsAttacking => !IsMove && target != null;
    float AttackDelay => 1 / attackSpeed;
    float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animHandle = GetComponent<AnimationHandler>();
        curHp = maxHp;
    }

    protected virtual void Update()
    {
        HandleAction();
        SetDir();
        HandleAttackDelay();
        Debug.DrawRay(transform.position, lookDir, Color.red);
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// �Է�/���ǿ� ���� ���� �ൿ�� �����մϴ�. �ַ� �̵������� ���� ���� �ֽ��ϴ�.
    /// </summary>
    protected virtual void HandleAction()
    { }

    protected virtual void Move()
    {
        animHandle?.Move(moveDir);

        if (rig != null)
            rig.velocity = moveDir * speed;
    }

    /// <summary>
    /// �̵��ÿ��� �̵�������, �� �ܿ��� ��ǥ���� �ٶ󺸵��� �մϴ�.
    /// </summary>
    protected virtual void SetDir()
    {
        if (target != null && !IsMove)
            lookDir = (target.position - transform.position).normalized;
        else if (IsMove)
            lookDir = moveDir;

        if (lookDir.x != 0)
            sprite.flipX = lookDir.x < 0;
    }

    /// <summary>
    /// ���ݼӵ��� �ݿ��� ���� ���ݱ��� ������ ����մϴ�.
    /// </summary>
    void HandleAttackDelay()
    {
        if (timeSinceLastAttack <= AttackDelay)
            timeSinceLastAttack += Time.deltaTime;
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    /// <summary>
    /// ���ݽ� �ൿ�� �����մϴ�.
    /// </summary>
    protected virtual void Attack()
    {
        animHandle?.Attack();
    }

    public virtual void CreateProjectile()
    {

    }

    /// <summary>
    /// ĳ������ ���� ü���� �����մϴ�.
    /// </summary>
    /// <param name="change">������ ��ġ�Դϴ�. �������� ����, ȸ���̸� ������� �Է��մϴ�.</param>
    public virtual void ChangeHealth(float change)
    {
        CurHp += change;
        HpBar.value = CurHp / maxHp;
        if (CurHp == 0f)
            Death();
    }

    /// <summary>
    /// ĳ���Ͱ� ����ϸ� ���ڸ��� �����ϰ� ��� �� ������ϴ�.
    /// </summary>
    protected virtual void Death()
    {
        rig.velocity = Vector2.zero;

        foreach (var compo in transform.GetComponentsInChildren<Behaviour>())
            compo.enabled = false;

        Destroy(gameObject, 2f);
    }
}
