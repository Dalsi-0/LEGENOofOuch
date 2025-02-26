using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private Collider2D confinerCollider;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        confinerCollider = transform.parent.GetComponent<StageContainer>().cameraCollider;
    }

    private void Start()
    {
        transform.GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform;
        SetupConfiner();
    }

    public void SetupConfiner()
    {
        if (confinerCollider == null)
        {
            Debug.LogError("Confiner Collider�� �������� �ʾҽ��ϴ�!");
            return;
        }
        confiner.m_BoundingShape2D = confinerCollider;
        confiner.InvalidateCache(); // ���� ���� �ݿ�
    }
}
