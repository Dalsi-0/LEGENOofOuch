using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage = 0.2f, criticalChance;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;

    //버프수치: 20%증가시 0.2f 입력
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


    /// <summary>
    /// 키보드 입력으로 이동방향을 결정합니다.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        SearchTarget();
    }

    /// <summary>
    /// 현제 활성화된 적들 중 가장 가까운 적을 목표로 삼습니다.
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
    /// 무적상태일 경우 체력회복만 가능합니다.
    /// </summary>
    /// <param name="change">변경할 수치입니다. 데미지면 음수, 회복이면 양수값을 입력합니다.</param>
    public override void ChangeHealth(float change)
    {
        if (!GodMod || change > 0)
            base.ChangeHealth(change);
    }

    /// <summary>
    /// 죽음에 달하면 목숨이 하나 줄고, 목숨이 다하면 사망합니다.
    /// </summary>
    protected override void Death()
    {
        if (--life > 0)
            return;
        //사망시 게임종료 로직 실행
        base.Death();
    }

    protected override void Attack()
    {
        base.Attack();
        GameManager.Instance.ProjectileManager.ShootPlayerProjectile(this.transform.position, lookDir,pClass,1,1);
    }

    private void Start()
    {
        GameManager.Instance.ProjectileManager.CreateFairy(this.transform.position);
    }
}
