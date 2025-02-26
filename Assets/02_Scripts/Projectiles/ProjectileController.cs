using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy; //�� ����
    [SerializeField] private LayerMask layerMaskWall; // �� ����
    [SerializeField] private LayerMask layerMaskTeam; // �Ʊ� ����
    private Rigidbody2D rigidbody2D;
    private Collider2D arrowCollider;
    private Vector3 direction; // �÷��̾��� ����
    private int contactWall; // ���� �浹 Ƚ��
    private int contactEnemy; // ���� �浹 Ƚ��
    private int contactWallCount; // �޾ƿ� �� �浹 Ƚ��
    private int contactEnemyCount; // �޾ƿ� �� �浹 Ƚ��
    private bool isDarkTouch;
    private bool isBlaze;
    private float arrowAttackPower;

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
    public void Init(Vector3 _direction, bool _isDarkTouch, bool _isBlaze, int _contactwallCount = 0, int _contactEnemyCount = 0)
    {
        direction = _direction;
        RotationRojectile();
        contactWallCount = _contactwallCount;
        contactEnemyCount = _contactEnemyCount;
        isDarkTouch = _isDarkTouch;
        isBlaze = _isBlaze;
        arrowAttackPower = GameManager.Instance.player.AttackPower;
    }

    void Update()
    {
        DirectionProjcetile();
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
        //if (collision.gameObject.GetComponent<ProjectileController>()) //�ٸ� ������Ʈ�� ProjectileController�� ������ ������ �Ѿ��̱� ������
        //{ Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer); } // ����
        //else // �׿�
        //{
        if (layerMaskWall.value == (layerMaskWall.value | (1 << collision.gameObject.layer))) // ���� �浹������
        {
            if (contactWall < contactWallCount) // ���� �浹Ƚ���� �޾ƿ� �浹Ƚ������ ���ٸ�
            {
                var contact = collision.contacts[0];
                // �浹 ����
                direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
                RotationRojectile();
                contactWall += 1;
                arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactWallDecreaseDamage();
            }
            else if (contactWall >= contactWallCount) // ���� �浹Ƚ���� �޾ƿ� �浹Ƚ���� ���ų� ũ�ٸ�
                Destroy(this.gameObject);
        }
        else if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer))) // ���� �浹������
        {
            if (contactEnemy < contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                enemy.ChangeHealth(-arrowAttackPower); // ���� �ٲ�� ����
                if(enemy != null)
                {
                    if (isDarkTouch) // ����� ���� ��ų ����
                    {
                        StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // ������ ����
                    {
                        StartCoroutine (BlazeDelay(enemy));
                    }
                }
                Physics2D.IgnoreCollision(arrowCollider, collision.collider);
                contactEnemy += 1;
                arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactEnemyDecreaseDamage();

            }
            else if (contactEnemy >= contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                enemy.ChangeHealth(-arrowAttackPower); // ���� �ٲ�� ����
                if (enemy != null)
                {
                    if (isDarkTouch) // ����� ���� ��ų ����
                    {
                        StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // ������ ����
                    {
                        StartCoroutine(BlazeDelay(enemy));
                    }
                }
                Destroy(this.gameObject);
            }
        }
        else if (layerMaskTeam.value == (layerMaskTeam.value | (1 << collision.gameObject.layer))) // ���� ���� ���
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer);
        }
        // }

        //if (contactWall < 2 && collision.gameObject.CompareTag("Wall")) // �ӽ÷� wall�� �ۼ� // ���ڿ��� �Ѿ� ƨ��� Ƚ������ �־��ָ��
        //{
        //    var contact = collision.contacts[0]; // �浹 ����
        //    direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
        //    RotationRojectile();
        //    contactWall += 1;
        //}
        //else if (contactWall >= 2 && collision.gameObject.CompareTag("Wall")) // �ӽ÷� wall�� �ۼ�
        //{
        //    Destroy(this.gameObject);
        //}
        //else if (contactEnemy < 2 && collision.gameObject.CompareTag("Enemy")) // �ӽ÷� Enemy�� �ۼ� 
        //{
        //    var contact = collision.contacts[0]; // �浹 ����
        //    direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
        //    RotationRojectile();
        //    contactEnemy += 1;
        //}
        //else if (contactEnemy >= 2 && collision.gameObject.CompareTag("Enemy")) // �ӽ÷� Enemy�� �ۼ�
        //{
        //    Destroy(this.gameObject);
        //}
    }


    /// <summary>
    /// ����� ���� ��ų ������
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DarkTouchDelay(EnemyCharacter enemy)
    {
        yield return new WaitForSeconds(1);
        enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage());
        List<EnemyCharacter> list = GameManager.Instance.MonsterManager.spawnedEnemys;
        float nearDir = 5000f;
        EnemyCharacter nearEnemy = null;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position != enemy.transform.position)
            {
                if (nearDir > Vector3.Distance(list[i].transform.position, enemy.transform.position))
                {
                    nearDir = Vector3.Distance(list[i].transform.position, enemy.transform.position);
                    nearEnemy = list[i];
                }
            }
        }
        if (nearEnemy == null)
        {
            Debug.Log("Projectile�� DarkTouchDelay�� nearEnemy�� null�Դϴ�!");
        }
        nearEnemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage());
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
            enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetBlazeDecresaseDamage());
        }
        yield return new WaitForSeconds(0.25f);
    }
    
}
