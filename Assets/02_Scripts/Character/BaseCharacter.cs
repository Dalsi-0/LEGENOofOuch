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
    /// 입력/조건에 따른 다음 행동을 결정합니다. 주로 이동방향의 결정 등이 있습니다.
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
    /// 이동시에는 이동방향을, 그 외에는 목표물을 바라보도록 합니다.
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
    /// 공격속도를 반영해 다음 공격까지 공격을 대기합니다.
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
    /// 공격시 행동을 정의합니다.
    /// </summary>
    protected virtual void Attack()
    {
        animHandle?.Attack();
        //gamemaneger.~~~
    }

    public virtual void CreateProjectile()
    {

    }

    /// <summary>
    /// 캐릭터의 현제 체력을 변경합니다.
    /// </summary>
    /// <param name="change">변경할 수치입니다. 데미지면 음수, 회복이면 양수값을 입력합니다.</param>
    public virtual void ChangeHealth(float change)
    {
        CurHp += change;
        HpBar.value = CurHp / maxHp;
        if (CurHp == 0f)
            Death();
    }

    /// <summary>
    /// 캐릭터가 사망하면 그자리에 정지하고 잠시 후 사라집니다.
    /// </summary>
    protected virtual void Death()
    {
        rig.velocity = Vector2.zero;

        foreach (var compo in transform.GetComponentsInChildren<Behaviour>())
            compo.enabled = false;

        Destroy(gameObject, 2f);
    }
}
