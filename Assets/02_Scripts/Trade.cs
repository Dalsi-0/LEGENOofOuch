using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    public Button acceptButton;
    public Button rejectButton;
    public GameObject devil;
    public void Awake()
    {

        acceptButton.onClick.AddListener(AcceptTrade);
        rejectButton.onClick.AddListener(RejectTrade);
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        devil = GameObject.Find("Devil");
    }
    /// <summary>
    /// �÷��̾ �ŷ���ư�� �������� ���ӸŴ������� ������ �����ش�.
    /// ���ӸŴ����� �÷��̾�� extralife�� �ְ� ����� 30�� �����´�.
    /// </summary>
    public void AcceptTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");
        //Ŭ������/����Ŭ������ ������ ã�ƿ��ڽ��ϴ�
        SoundManager.instance.PlaySFX("");
        Destroy(devil);
        //GameManager.instance.Trade();
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// ui��Ȱ��ȭ �ǰ� ��
    /// </summary>
    public void RejectTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");
        //Ŭ������/����Ŭ������ ������ ã�ƿ��ڽ��ϴ�
        SoundManager.instance.PlaySFX("");
        Destroy(devil);
        this.gameObject.SetActive(false);
    }
}
