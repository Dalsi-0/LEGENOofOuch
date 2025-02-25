using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs; // Ŭ������ ����ü ������
    [SerializeField] private GameObject monsterProjectilePrefab; // ���ͺ� ����ü ������
    [SerializeField] private GameObject fairyPrefab; // ���� ������
    [SerializeField] private GameObject fireOrbPrefab; // ���� �� ������
    [SerializeField] private GameObject fairyProjectilePrefab; // ���� ������


    /// <summary>
    /// �Ѿ� ����, ����Ʈ�� ������, ���ڵ�, �ü� ������� ������ �־����
    /// </summary>
    /// <param name="startPosition">���� ��ġ</param>
    /// <param name="playerClass">�÷��̾� Ŭ����</param>
    /// <param name="direction"></param>
    public void ShootPlayerProjectile(Vector3 startPosition, Vector3 direction, PlayerClassEnum playerClass, int contactWallCount, int contactEnemyCount)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
        
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, contactWallCount,contactEnemyCount);
    }

    /// <summary>
    /// Enemy �Ѿ� ����
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="direction"></param>
    public void ShootEnemyProjectile(Vector3 startPosition, Vector3 direction)
    {
        GameObject origin = monsterProjectilePrefab;
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileEnemyController projectileEnemyController = obj.GetComponent<ProjectileEnemyController>();
        projectileEnemyController.Init(direction);
    }

    /// <summary>
    /// ��ų ���� �� ����
    /// </summary>
    /// <param name="playerPosition"></param>
    public void CreateFireOrb(Vector3 playerPosition)
    {
        GameObject origin = fireOrbPrefab;
        GameObject obj1 = Instantiate(origin, playerPosition, Quaternion.identity);
        GameObject obj2 = Instantiate(origin, playerPosition, Quaternion.identity);

        SurroundController fireOrbController1 = obj1.GetComponent<SurroundController>();
        SurroundController fireOrbController2 = obj2.GetComponent<SurroundController>();
        fireOrbController1.Init(0);
        fireOrbController2.Init(180);
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    /// <param name="playerPosition"></param>
    public void CreateFairy(Vector3 playerPosition)
    {
        GameObject origin = fairyPrefab;
        GameObject obj = Instantiate (origin, playerPosition, Quaternion.identity);

        SurroundController fairyController = obj.GetComponent<SurroundController>();
        fairyController.Init(270);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="fairyPosition">���� ��ġ</param>
    /// <param name="direction">ǥ���� ������ ��ġ ����</param>
    public void ShootFairy(Vector3 fairyPosition, Vector3 direction)
    {
        GameObject origin = fairyProjectilePrefab;
        GameObject obj = Instantiate(origin, fairyPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction);
    }

}
