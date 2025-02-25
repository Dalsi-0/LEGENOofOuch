using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevilInteraction : MonoBehaviour
{
    public GameObject tradeUI;  // �ŷ� UI�� �����ϴ� ����

    /// <summary>
    /// �Ǹ����忡�� �÷��̾ �Ǹ��� ���������� trade���ष
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ���� ������Ʈ�� 'Player' �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �ŷ� UI Ȱ��ȭ
            tradeUI.SetActive(true);
        }
    }
}
