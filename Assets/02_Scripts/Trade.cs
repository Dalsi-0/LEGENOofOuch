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

    public void AcceptTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");
    }
    public void RejectTrade()
    {
        Debug.Log("�ŷ��� �����߽��ϴ�.");
    }
}
