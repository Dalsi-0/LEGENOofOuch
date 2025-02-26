using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage = 0.2f, criticalChance;
    [SerializeField] int level;
    [SerializeField] int exp;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;

    //������ġ: 20%������ 0.2f �Է�
    public float MaxHpBuf { get; set; }
    public float SpeedBuf { get; set; }
    public float AtkBuf { get; set; }
    public float AsBuf { get; set; }
    public float CriDmgBuf { get; set; }
    public float CriChanceBuf { get; set; }

    public override float MaxHp => base.MaxHp * (1 + MaxHpBuf);
    public override float Speed => base.Speed * (1 + SpeedBuf);
    public override float AttackPower => base.AttackPower * (1 + AtkBuf);
    public override float AttackSpeed => base.AttackSpeed * (1 + AsBuf);
    public float CriDmg => criticalDamage + CriDmgBuf;
    public float CriChance => criticalChance + CriChanceBuf;

    public bool GodMod = false;
    public int life = 1;

    // ��Ƽ��
    public bool isMultiShot = false;

    bool playerPaused = false;

    /// <summary>
    /// Ű���� �Է����� �̵������� �����մϴ�.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        if (!playerPaused)
            moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        SearchTarget();
    }

    /// <summary>
    /// ���� Ȱ��ȭ�� ����ִ� ���� �� ���� ����� ���� ��ǥ�� ����ϴ�.
    /// </summary>
    void SearchTarget()
    {
        target = null;
        var enemys = FindObjectsOfType<EnemyCharacter>();
        foreach (var enemy in enemys)
        {
            var enemyCharacter = enemy.GetComponent<EnemyCharacter>();
            float distance = (enemyCharacter.gameObject.transform.position - transform.position).magnitude;

            if (distance < TargetDis && enemyCharacter.GetCurHp() != 0)
                target = enemy.GameObject().transform;
        }
    }

    /// <summary>
    /// ���������� ��� ü��ȸ���� �����մϴ�.
    /// </summary>
    /// <param name="change">������ ��ġ�Դϴ�. �������� ����, ȸ���̸� ������� �Է��մϴ�.</param>
    public override void ChangeHealth(float change)
    {
        if (!GodMod || change > 0)
            base.ChangeHealth(change);
    }

    /// <summary>
    /// ������ ���ϸ� ����� �ϳ� �ٰ�, ����� ���ϸ� ����մϴ�.
    /// </summary>
    protected override void Death()
    {
        if (--life > 0)
        {
            ChangeHealth(MaxHp);
            return;
        }
        //����� �������� ���� ����
        base.Death();
    }
    protected override void Attack()
    {
        base.Attack();

        GameManager.Instance.AbilityManager.UseAbility();
        if (GameManager.Instance.AbilityManager.GetMultiShotOn())
        {
            StartCoroutine(AttackWithDelay(0.1f));
        }
    }

    IEnumerator AttackWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.AbilityManager.UseAbility();
    }

    protected override void Move()
    {
        base.Move();

        if (IsMove)
        {
            StopAllCoroutines();
        }
    }

    public void GetExp(int exp)
    {
        Debug.Log($"{exp}exp get");
        this.exp += exp;
        int upLv = this.exp / 100;
        level += upLv;
        for (int i = 0; i < upLv; i++)
            ChangeHealth(MaxHp / 10);
        this.exp %= 100;
    }

    public PlayerClassEnum GetPlayerClass()
    {
        return pClass;
    }

    /// <summary>
    /// ������ �ִ� ��� ���������� 0���� �ϰ� ü���� �ִ�� �մϴ�.
    /// </summary>
    public void ClearPlayerBuf()
    {
        MaxHpBuf = SpeedBuf = AtkBuf = AsBuf = CriDmgBuf = CriChanceBuf = 0;
        GodMod = isMultiShot = false;
        life = 1;
        playerPaused = false;
        CurHp = MaxHp;
    }

    public void SetClass(PlayerClassEnum pClass)
    {
        this.pClass = pClass;
        var pForm = GetComponent<PlayerFormChange>();
        pForm.FormChange(pClass);
    }

    /// <summary>
    /// �������� ���� ���� �÷��̾ �Ͻ�����/��ų ȹ�� �� ��ü�մϴ�.
    /// </summary>
    public void PauseControll()
    {
        playerPaused = !playerPaused;
        var collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = playerPaused;
    }
}
