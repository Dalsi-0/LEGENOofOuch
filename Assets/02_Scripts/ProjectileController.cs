using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 playerDirection;
    public GameObject one;        //test�뵵
    public GameObject two;        //test�뵵
    private Vector2 direction;    //test�뵵
    private int contactWall = 0; // ���� �浹 Ƚ��
    private int contactEnemy = 0; // ���� �浹 Ƚ��

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        // playerDirection = GameManager.Instance.Player.Direction; 
        direction = one.transform.position - two.transform.position;
        RotationRojectile();
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
        //rigidbody2D.velocity = playerDirection * 10f;
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
        if (contactWall < 2 && collision.gameObject.CompareTag("Wall")) // �ӽ÷� wall�� �ۼ� // ���ڿ��� �Ѿ� ƨ��� Ƚ������ �־��ָ��
        {
            var contact = collision.contacts[0]; // �浹 ����
            direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
            RotationRojectile();
            contactWall += 1;
        }
        else if (contactWall >= 2 && collision.gameObject.CompareTag("Wall")) // �ӽ÷� wall�� �ۼ�
        {
            Destroy(this.gameObject);
        }

        if (contactEnemy < 2 && collision.gameObject.CompareTag("Enemy")) // �ӽ÷� Enemy�� �ۼ� 
        {
            var contact = collision.contacts[0]; // �浹 ����
            direction = Vector3.Reflect(direction, contact.normal); // ���� �������� �浹������ ����� �ݻ簢�� ������
            RotationRojectile();
            contactEnemy += 1;
        }
        else if (contactEnemy >= 2 && collision.gameObject.CompareTag("Enemy")) // �ӽ÷� Enemy�� �ۼ�
        {
            Destroy(this.gameObject);
        }
    }
}
