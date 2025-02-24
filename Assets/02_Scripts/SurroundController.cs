using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundController : MonoBehaviour
{
    private float circleRadius = 1f; // ������
    private float deg = 0; // ����
    private float speed = 40f;  // �̵� �ӵ�
    private Vector3 playerPosition; // �÷��̾� ������

    public void Init(Vector3 _playerPosition, float chagedeg = 0)
    {
        playerPosition = _playerPosition;
        deg = chagedeg;
    }
    // Update is called once per frame
    void Update()
    {
        SurroundPosition();
    }

    /// <summary>
    /// ������ ȸ����Ű�� �޼���
    /// </summary>
    private void SurroundPosition()
    {
        deg += Time.deltaTime * speed;
        if (deg < 360)
        {
            float rad = Mathf.Deg2Rad * (deg); //1��(degree)�� �������� ��ȯ�ϴ� ���(�� / 180) �̹Ƿ�, deg ���� ���ϸ� ���� ���� ���� �� ����, rad���ش� ���������� ��ġ�� ����ϱ� ���� ���� ��
            float x = circleRadius * Mathf.Cos(rad); // x�� y��ġ�� �ٲٸ� �ð�������� ȸ��
            float y = circleRadius * Mathf.Sin(rad);
            this.transform.position = playerPosition + new Vector3(x, y, 0);
        }
        else { deg = 0; }
    }
}
