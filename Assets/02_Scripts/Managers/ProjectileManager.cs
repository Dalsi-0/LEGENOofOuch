using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private GameObject monsterProjectilePrefabs;

    /// <summary>
    /// �Ѿ� ����, ����Ʈ�� ������, ���ڵ�, �ü� ������� ������ �־����
    /// </summary>
    /// <param name="startPosition">���� ��ġ</param>
    /// <param name="playerClass">�÷��̾� Ŭ����</param>
    /// <param name="direction"></param>
    public void ShootPlayerProjectile(Vector2 startPosition, Vector2 direction, PlayerClassEnum playerClass, int contactWallCount, int contactEnemyCount)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
        
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, contactWallCount,contactEnemyCount);
    }

    public void ShootEnemyProjectile(Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = monsterProjectilePrefabs;
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction);
    }
}
