using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera thisCam;
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private Collider2D confinerCollider;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        confinerCollider = transform.parent.GetComponent<StageContainer>().cameraCollider;
        GameManager.Instance.CameraManager.SetVirtualCam(thisCam);
    }

    /// <summary>
    /// Ÿ�ϸ� �������� Ȱ��ȭ�ɶ� �ش� Ÿ�ϸ��� �����ī�޶� ����
    /// </summary>
    private void Start()
    {
        transform.GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform;
        SetupConfiner();
    }

    /// <summary>
    /// �ó׸ӽ��� Confiner ����� ����ϱ����� �ݶ��̴� ���� ����
    /// </summary>
    public void SetupConfiner()
    {
        if (confinerCollider == null)
        {
            return;
        }
        confiner.m_BoundingShape2D = confinerCollider;
        confiner.InvalidateCache(); // ���� ���� �ݿ�
    }
}
