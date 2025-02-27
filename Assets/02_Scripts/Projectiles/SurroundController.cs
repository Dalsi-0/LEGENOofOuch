using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundController : MonoBehaviour
{
    private float circleRadius = 1f; // ������
    private float deg = 0; // ����
    private float speed = 40f;  // �̵� �ӵ�
    private float timeSinceLastAttack = 0; //���� �ð�
    private float AttackDelay = 5f; // ���� ������
    private bool IsAttacking = true; // ���� ����

    /// <summary>
    /// ���� �޾ƿ�
    /// </summary>
    /// <param name="chagedeg"></param>
    public void Init(float chagedeg = 0)
    {
        deg = chagedeg;
    }
    

    /// <summary>
    /// 5�ʸ��� �����ϰ� �÷��̾� �ֺ��� ���ƴٴ�
    /// </summary>
    void Update()
    {
        SurroundPosition();
        if (timeSinceLastAttack <= AttackDelay) // ������ 
            timeSinceLastAttack += Time.deltaTime;
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0;
            if (GameManager.Instance.player.target == null)
            {
                Debug.Log("������ ������ ����� �����ϴ�.");
            }
            else { GameManager.Instance.ProjectileManager.ShootFairy(this.transform.position, (GameManager.Instance.player.target.position - this.transform.position)); }
        }

    }

    /// <summary>
    /// ������ �÷��̾� �ֺ��� ȸ����Ű�� �޼���
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
}
