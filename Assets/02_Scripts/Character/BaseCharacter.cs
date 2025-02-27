using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    AnimationHandler animHandle;
    [SerializeField] public Transform target;
    [SerializeField] Slider HpBar;

    public SpriteRenderer Sprite => sprite;

    [Header("BaseStat")]
    [SerializeField] float maxHp = 100;
    [SerializeField] float speed = 2;
    [SerializeField] float attackPower = 1, attackSpeed = 1;

    public virtual float MaxHp => maxHp;
    public virtual float Speed => speed;
    public virtual float AttackPower => attackPower;
    public virtual float AttackSpeed => attackSpeed;

    float curHp;
    public float CurHp { get => curHp; protected set => curHp = Mathf.Clamp(value, 0, MaxHp); }

    protected Rigidbody2D rig;
    protected Vector2 lookDir, moveDir;
    public Vector2 LookDir => lookDir;
    public Vector2 MoveDir => moveDir;
    protected bool IsMove => MoveDir.magnitude > 0.5f;
    protected float TargetDis => target == null ? float.MaxValue : (target.position - transform.position).magnitude;

    protected virtual bool IsAttacking => !IsMove && target != null;
    float AttackDelay => 1 / AttackSpeed;
    float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animHandle = GetComponent<AnimationHandler>();
        curHp = MaxHp;
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
            rig.velocity = moveDir * Speed;
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
        animHandle?.Attack(AttackSpeed);
        //����ü �߻�� ������ ȿ����
        SoundManager.instance.PlaySFX("");
        //gamemaneger.~~~
    }

    /// <summary>
    /// ĳ������ ���� ü���� �����մϴ�.
    /// </summary>
    /// <param name="change">������ ��ġ�Դϴ�. �������� ����, ȸ���̸� ������� �Է��մϴ�.</param>
    public virtual void ChangeHealth(float change)
    {
        //�ǰ�/ȸ���� ������ ȿ����
        string changeSfx = change >= 0 ? "" :"";
        SoundManager.instance.PlaySFX(changeSfx);

        CurHp += change;
        HpBar.value = CurHp / MaxHp;
        if (CurHp == 0f)
            Death();
    }

    /// <summary>
    /// ĳ���Ͱ� ����ϸ� ���ڸ��� �����ϰ� ��� �� ������ϴ�.
    /// </summary>
    protected virtual void Death()
    {
        //����� ������ ȿ����
        SoundManager.instance.PlaySFX("");

        rig.velocity = Vector2.zero;

        foreach (var compo in transform.GetComponentsInChildren<Behaviour>())
            compo.enabled = false;

        Destroy(gameObject);
    }

    /// <summary>
    /// ���� HP ��ȯ
    /// </summary>
    /// <returns></returns>
    public float GetCurHp()
    {
        return CurHp;
    }

    /// <summary>
    /// ���� �ٶ󺸴� ���� vector
    /// </summary>
    /// <returns></returns>
    public Vector3 GetlookDir()
    {
        return lookDir;
    }

    public SpriteRenderer GetPlayerSpriteRenderer()
    {
        return sprite;
    }
}
