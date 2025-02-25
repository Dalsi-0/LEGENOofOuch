using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilInteraction : MonoBehaviour
{
    public GameObject tradeUI;  // �ŷ� UI�� �����ϴ� ����

    // �Ǹ��� �浹�� �߻����� �� (2D �浹 ó��)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ���� ������Ʈ�� 'Player' �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�΋H!");
            // �ŷ� UI Ȱ��ȭ
            tradeUI.SetActive(true);
        }
    }
}
