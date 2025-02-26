using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    public Button acceptButton;
    public Button rejectButton;

    public void Awake()
    {

        acceptButton.onClick.AddListener(AcceptTrade);
        rejectButton.onClick.AddListener(RejectTrade);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �÷��̾ �ŷ���ư�� �������� ���ӸŴ������� ������ �����ش�.
    /// ���ӸŴ����� �÷��̾�� extralife�� �ְ� ����� 30�� �����´�.
    /// </summary>
    public void AcceptTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");

        SoundManager.instance.PlaySFX("Ŭ������/����Ŭ������ ������ ã�ƿ��ڽ��ϴ�.");
        //GameManager.instance.Trade();
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// ui��Ȱ��ȭ �ǰ� ��
    /// </summary>
    public void RejectTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");

        SoundManager.instance.PlaySFX("Ŭ������/����Ŭ������ ������ ã�ƿ��ڽ��ϴ�.");
        this.gameObject.SetActive(false);
    }
}
