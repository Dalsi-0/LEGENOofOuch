using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;

    /// <summary>
    /// �Ѿ� ����, ����Ʈ�� ������, ���ڵ�, �ü� ������� ������ �־����
    /// </summary>
    /// <param name="startPosition">�÷��̾� ��ġ</param>
    /// <param name="playerClass">�÷��̾� Ŭ����</param>
    public void ShootProjectile(Vector2 startPosition, PlayerClassEnum playerClass)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
    }
}
