using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy; //�� ����
    [SerializeField] private LayerMask layerMaskWall; // �� ����
    [SerializeField] private LayerMask layerMaskTeam; // �Ʊ� ���� �� �Ѿ� �ΰ� ���̾� �־��ָ��
    private Rigidbody2D rigidbody2D;
    private Collider2D arrowCollider;
    private Vector3 direction; // �÷��̾��� ����
    private int contactWall =0; // ���� �浹 Ƚ��
    private int contactEnemy= 0; // ���� �浹 Ƚ��
    private int contactWallCount; // �޾ƿ� �� �浹 Ƚ��
    private int contactEnemyCount; // �޾ƿ� �� �浹 Ƚ��
    private bool isDarkTouch; // ����� ���� on off
    private bool isBlaze; // ������ on off
    private float arrowAttackPower; // ȭ�� ���ݷ�
    private float arrowDestoryTime = 0f; // ȭ�� �ı� �ð�
    private float arrowStayTime = 6f; // ȭ�� ���� �ð�


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        arrowCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// �Ѿ� �ʱ�ȭ �޼���
    /// </summary>
    /// <param name="_direction">����</param>
    /// <param name="_contactwallCount">�� �浹Ƚ��, ���� ��� 0 </param>
    /// <param name="_contactEnemyCount">�� �浹Ƚ��, ���̽�� 0 </param>
    public void Init(Vector3 _direction, bool _isDarkTouch, bool _isBlaze, float _attackPower ,int _contactwallCount = 0, int _contactEnemyCount = 0)
    {
        direction = _direction;
        RotationRojectile();
        contactWallCount = _contactwallCount;
        contactEnemyCount = _contactEnemyCount;
        isDarkTouch = _isDarkTouch;
        isBlaze = _isBlaze;
        arrowAttackPower = _attackPower * GameManager.Instance.ProjectileManager.GetFinalDecreaseDamage();
    }

    void Update()
    {
        DirectionProjcetile();
        if (arrowDestoryTime >= arrowStayTime)
        {
            Destroy(this.gameObject);
        }
        else { arrowDestoryTime += Time.deltaTime; }

    }

    /// <summary>
    /// �Ѿ����� �޼���
    /// </summary>
    public void DirectionProjcetile()
    {
        rigidbody2D.velocity = direction.normalized * 10f;
        
    }

    /// <summary>
    /// �Ѿ� ȸ�� �޼���
    /// direction playerDirection���� �ٲ���� �׽�Ʈ�� ���� direction���� �س��� �� 
    /// </summary>
    public void RotationRojectile()
    {
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //ȸ�� ���� ���

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(180f, 0, -rotationZ);
        }
        else { transform.rotation = Quaternion.Euler(0, 0, rotationZ); }

    }

    /// <summary>
    /// �΋H������
    /// </summary>
    /// <param name="collision">�� Ȥ�� ��</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((layerMaskWall.value & (1 << collision.gameObject.layer)) != 0) // ���� �浹������
        {
            if (contactWall < contactWallCount) // ���� �浹Ƚ���� �޾ƿ� �浹Ƚ������ ���ٸ�
            {
                if (collision.contacts.Length > 0)
                {
                    var contact = collision.contacts[0];// �浹 ����
                    direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
                    RotationRojectile();
                    contactWall += 1;
                    arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactWallDecreaseDamage();
                }
            }
            if (contactWall >= contactWallCount) // ���� �浹Ƚ���� �޾ƿ� �浹Ƚ���� ���ų� ũ�ٸ�
                Destroy(this.gameObject);
        }
        if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer))) // ���� �浹������
        {
            if (contactEnemy < contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                CirticalPer(enemy); 
                if (enemy != null)
                {
                    if (isDarkTouch) // ����� ���� ��ų ����
                    {
                        GameManager.Instance.StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // ������ ����
                    {
                        GameManager.Instance.StartCoroutine (BlazeDelay(enemy));
                    }
                }
                Physics2D.IgnoreCollision(arrowCollider, collision.collider);
                contactEnemy += 1;
                arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactEnemyDecreaseDamage();

            }
            else if (contactEnemy >= contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                CirticalPer(enemy); 
                if (enemy != null)
                {
                    if (isDarkTouch) // ����� ���� ��ų ����
                    {
                        GameManager.Instance.StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // ������ ����
                    {
                        GameManager.Instance.StartCoroutine(BlazeDelay(enemy));
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (layerMaskTeam.value == (layerMaskTeam.value | (1 << collision.gameObject.layer))) // ���� ���� ���
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer);
        }
    }

    /// <summary>
    /// ũ��Ƽ�� ���
    /// </summary>
    /// <param name="enemy"></param>
    private void CirticalPer(EnemyCharacter enemy)
    {
        if (GameManager.Instance.player.CriChance < Random.Range(0.01f, 1))
            enemy.ChangeHealth(-(arrowAttackPower + (GameManager.Instance.player.CriDmg)*arrowAttackPower));
        else
            enemy.ChangeHealth(-arrowAttackPower);
    }


    /// <summary>
    /// ����� ���� ��ų ������
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DarkTouchDelay(EnemyCharacter enemy)
    {
        yield return new WaitForSeconds(1);
        if (enemy != null)
        {
            enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage());

            List<EnemyCharacter> list = GameManager.Instance.MonsterManager.spawnedEnemys; // ���� ����Ʈ ������
            float nearDir = 5000f;
            EnemyCharacter nearEnemy = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].transform.position != enemy.transform.position)
                {
                    if (nearDir > Vector3.Distance(list[i].transform.position, enemy.transform.position)) // ������ ��ó �Ÿ����� ������ �Ÿ��� �� ������ ����
                    {
                        nearDir = Vector3.Distance(list[i].transform.position, enemy.transform.position); //����� �Ÿ��� ������� �Ÿ��� �ְ� �ش� ���͸� ������
                        nearEnemy = list[i];
                    }
                }
            }

            if (nearEnemy == null)
            {
            }
            else { nearEnemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage()); }
        }
    }

    /// <summary>
    /// ������ ��ų ������
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator BlazeDelay(EnemyCharacter enemy)
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.25f);
            if (enemy == null) { break; }
            enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetBlazeDecresaseDamage());
        }
        yield return new WaitForSeconds(0.25f);
    }
    
}
