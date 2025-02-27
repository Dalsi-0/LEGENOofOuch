using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevilInteraction : MonoBehaviour
{
    public GameObject tradeUI; 


    /// <summary>
    /// �Ǹ����忡�� �÷��̾ �Ǹ��� ���������� trade����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tradeUI.SetActive(true);
            SoundManager.instance.PlaySFX("�Ǹ������Ҹ� SFX�ȿ� �־�J���ϴ�");
        }
    }
}
