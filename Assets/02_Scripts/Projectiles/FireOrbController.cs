using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrbController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy;
    private float circleRadius = 1f; // ������
    private float deg = 0; // ����
    private float speed = 40f;  // �̵� �ӵ�

    public void Init(float chagedeg = 0)
    {
        deg = chagedeg;
    }

    void Update()
    {
        SurroundPosition();
    }

    /// <summary>
    /// ������ �÷��̾� �ֺ� ȸ���ϴ� �޼���
    /// </summary>
    private void SurroundPosition()
    {
        deg += Time.deltaTime * speed;
        if (deg < 360)
        {
            float rad = Mathf.Deg2Rad * (deg); //1��(degree)�� �������� ��ȯ�ϴ� ���(�� / 180) �̹Ƿ�, deg ���� ���ϸ� ���� ���� ���� �� ����, rad���ش� ���������� ��ġ�� ����ϱ� ���� ���� ��
            float x = circleRadius * Mathf.Cos(rad); // x�� y��ġ�� �ٲٸ� �ð�������� ȸ��
            float y = circleRadius * Mathf.Sin(rad);
            if (GameManager.Instance.player == null)
            {
                Destroy(this.gameObject);
            }
            else
                this.transform.position = GameManager.Instance.player.transform.position + new Vector3(x, y, 0);
        }
        else { deg = 0; }
    }


    /// <summary>
    /// ���� ���������� ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer)))
        {
            EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
            enemy.ChangeHealth(-(GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetFireOrbDecreaseDamage())); 
        }
    }

}
