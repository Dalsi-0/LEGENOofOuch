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
        devil = GameObject.FindGameObjectWithTag("Devil");//�ŷ��Ϸ��� �Ǹ� ������Ʈ�� ó���ϱ�����
    }
    /// <summary>
    /// �÷��̾ �ŷ���ư�� �������� ���ӸŴ������� ������ �����ش�.
    /// ���ӸŴ����� �÷��̾�� extralife�� �ְ� ����� 30�� �����´�.
    /// �Ǹ�������Ʈ ����
    /// </summary>
    public void AcceptTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");
        //Ŭ������/����Ŭ������ ������ ã�ƿ��ڽ��ϴ�
        SoundManager.instance.PlaySFX("");
        GameManager.Instance.Trade();
        Achievements.TriggerFirstTrade();
        Destroy(devil);
        //GameManager.instance.Trade();
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// ui��Ȱ��ȭ
    /// �Ǹ�������Ʈ ����
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
