using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy; //�� ����
    [SerializeField] private LayerMask layerMaskWall; // �� ����
    [SerializeField] private LayerMask layerMaskTeam; // �Ʊ� ���� �� �Ѿ˼��� layer�� �Ʊ��� projectile �ΰ� �־��ָ��
    private Rigidbody2D rigidbody2D;
    private Vector3 direction; // �÷��̾��� ����
    private int contactWall; // ���� �浹 Ƚ��
    private int contactEnemy; // ���� �浹 Ƚ��
    private int contactWallCount; // �޾ƿ� �� �浹 Ƚ��
    private int contactEnemyCount; // �޾ƿ� �� �浹 Ƚ��
    private float damage; // ������
    private float arrowDestoryTime = 0f; // �Ѿ� �ı� �ð�
    private float arrowStayTime = 6f; // �Ѿ� ���ӽð�

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// �Ѿ� �ʱ�ȭ �޼���
    /// </summary>
    /// <param name="_direction">����</param>
    /// <param name="_contactwallCount">�� �浹Ƚ��, ���� ��� 0 </param>
    /// <param name="_contactEnemyCount">�� �浹Ƚ��, ���̽�� 0 </param>
    public void Init(Vector3 _direction, float _damage, int _contactwallCount = 0, int _contactEnemyCount = 0)
    {
        direction = _direction;
        RotationRojectile();
        contactWallCount = _contactwallCount;
        contactEnemyCount = _contactEnemyCount;
        damage = _damage;
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
            if (collision.contacts.Length > 0) // ���� ��ų������ ����� ���ܵ� �ڵ�
            {
                if (contactWall < contactWallCount) // ���� �浹Ƚ���� �޾ƿ� �浹Ƚ������ ���ٸ�
                {
                    var contact = collision.contacts[0]; // �浹 ����

                    direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
                    RotationRojectile();
                    contactWall += 1;
                }
            }
            if (contactWall >= contactWallCount) // ���� �浹Ƚ���� �޾ƿ� �浹Ƚ���� ���ų� ũ�ٸ�
                Destroy(this.gameObject);
        }
        else if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer))) // �÷��̾�� �浹������
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            player.ChangeHealth(-damage);
            Destroy(this.gameObject);

        }
        else if (layerMaskTeam.value == (layerMaskTeam.value | (1 << collision.gameObject.layer))) // �Ʊ��Ѿ�, ���Ѿ�, �����̾� �����ؼ� ���� 
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer);
        }
    }
}
