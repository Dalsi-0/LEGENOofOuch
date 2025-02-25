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


    /// <summary>
    /// Ű���� �Է����� �̵������� �����մϴ�.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        SearchTarget();
    }

    /// <summary>
    /// ���� Ȱ��ȭ�� ���� �� ���� ����� ���� ��ǥ�� ����ϴ�.
    /// </summary>
    void SearchTarget()
    {
        target = null;
        var enemys = FindObjectsOfType(typeof(EnemyCharacter));
        foreach (var enemy in enemys)
        {
            float distance = (enemy.GameObject().transform.position - transform.position).magnitude;

            if (distance < TargetDis)
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
            return;
        //����� �������� ���� ����
        base.Death();
    }

    protected override void Attack()
    {
        base.Attack();

        GameManager.Instance.AbilityManager.UseAbility();
    }

    public void GetExp(int exp)
    {
        this.exp += exp;
        int upLv = exp / 100;
        level += upLv;
        for (int i = 0; i < upLv; i++)
            ChangeHealth(maxHp / 10);
        exp %= 100;
    }

    public PlayerClassEnum GetPlayerClass()
    {
        return pClass;
    }
}
