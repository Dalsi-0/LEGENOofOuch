using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage = 0.2f, criticalChance;
    [SerializeField] int level = 1;
    [SerializeField] int exp;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;
    [SerializeField] Slider expbar;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] ParticleSystem healParticle;

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

    //�÷��̾��� �Ͻ��������¸� Ȯ���ϴ� �����Դϴ�.
    bool playerPaused = false;
    public bool PlayerPaused
    {
        get => playerPaused;
        set
        {
            moveDir = Vector2.zero;
            playerPaused = value;
            var collider = GetComponent<BoxCollider2D>();
            var rigi = GetComponent<Rigidbody2D>();
            collider.enabled = !playerPaused;
            rigi.simulated = !playerPaused;
        }
    }

    /// <summary>
    /// Ű���� �Է����� �̵������� �����մϴ�.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();

        // Ű ���ε����� ����� ���� Ű ��������
        bool moveUp = Input.GetKey(OptionManager.instance.GetKey("Up"));
        bool moveDown = Input.GetKey(OptionManager.instance.GetKey("Down"));
        bool moveLeft = Input.GetKey(OptionManager.instance.GetKey("Left"));
        bool moveRight = Input.GetKey(OptionManager.instance.GetKey("Right"));

        moveDir.Set((moveRight ? 1 : 0) - (moveLeft ? 1 : 0),
                    (moveUp ? 1 : 0) - (moveDown ? 1 : 0));

        moveDir.Normalize();
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
        {
            base.ChangeHealth(change);

            // ü�� ȸ���� ���� ��ƼŬ ����
            if (change > 0 && healParticle != null)
            {
                healParticle.Play();
            }
        }
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
        Achievements.TriggerFirstDeath();
        base.Death();
        GameManager.Instance.EndGame();
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

    /// <summary>
    /// �̵��ÿ��� ��ų�� ������� �ʽ��ϴ�.
    /// </summary>
    protected override void Move()
    {
        base.Move();

        if (IsMove)
            StopAllCoroutines();
    }

    public void GetExp(int exp)
    {
        this.exp += exp;

        int upLv = this.exp / 100;

        level += upLv;
        if (upLv > 0)
            Achievements.TriggerFirstLevelUp();
        for (int i = 0; i < upLv; i++)
            ChangeHealth(MaxHp / 10);
        this.exp %= 100;

        expbar.value = this.exp / 100f;
        levelTxt.text = level.ToString();
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
        PlayerPaused = false;
        CurHp = MaxHp;
        exp = 0;
        level = 1;
    }

    /// <summary>
    /// ���õ� ������ �ݿ����ݴϴ�.
    /// </summary>
    /// <param name="pClass">���� �÷��̾��� �����Դϴ�.</param>
    public void SetClass(PlayerClassEnum pClass)
    {
        this.pClass = pClass;
        var pForm = GetComponent<PlayerFormChange>();
        pForm.FormChange(pClass);
    }
}
