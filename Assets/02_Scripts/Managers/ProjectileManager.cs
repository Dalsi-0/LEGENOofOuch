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

    private int contactWallCount;
    private int contactEnemyCount;
    private float contactWallDecreaseDamage;
    private float contactEnemyDecreaseDamage;
    private float finalDecreaseDamage;
    private float darkTouchDecreaseDamage;
    private float blazeDecresaseDamage;
    private float fireOrbDecreaseDamage;
    private float fairyDecreaseDamage;
    private bool isDarkTouch = false;
    private bool isBlaze = false;
    private bool isDeathBoom = false;
    



    /// <summary>
    /// �Ѿ� ����, ����Ʈ�� ������, ���ڵ�, �ü� ������� ������ �־����
    /// </summary>
    /// <param name="startPosition">���� ��ġ</param>
    /// <param name="playerClass">�÷��̾� Ŭ����</param>
    /// <param name="direction"></param>
    public void ShootPlayerProjectile(Vector3 startPosition, Vector3 direction, PlayerClassEnum playerClass)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, isDarkTouch, isBlaze, GameManager.Instance.player.AttackPower, contactWallCount, contactEnemyCount);
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
        GameObject obj = Instantiate(origin, playerPosition, Quaternion.identity);

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
        projectileController.Init(direction, false, false, GameManager.Instance.player.AttackPower* fairyDecreaseDamage);
        
    }

    public GameObject ShootBigSwordAura(Vector3 startPosition, Vector3 direction, PlayerClassEnum playerClass)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, isDarkTouch, isBlaze, contactWallCount, contactEnemyCount);
        float x = obj.transform.localScale.x * 2; // ũ�� 100% ����
        float y = obj.transform.localScale.y * 2;
        obj.transform.localScale = new Vector3(x, y, 1);

        return obj;
    }



    /// <summary>
    /// �Ʒ� �������� �������ų� �������� �Լ�
    /// contactWallCount; // �� �ݵ� Ƚ��
    /// contactEnemyCount; // �� ���� Ƚ��
    /// contactWallDecreaseDamage; // �� �ݵ��� ������ ���ҷ�
    /// contactEnemyDecreaseDamage; // �� ����� ������ ���ҷ�
    /// finalDecreaseDamage; // ���������� ���ҷ�
    /// darkTouchDecrreaseDamage; // ����� ���� ������ ���ҷ�
    /// isDeathBoom; // ������ bool��
    /// BlazeDecresaseDamage; // ������ ������ ���ҷ�
    /// fireOrbDecreaseDamage; // ���ǿ� ������ ���ҷ�
    /// fairyDecreaseDamage // ���� ������ ���ҷ�
    /// </summary>
    /// <returns></returns>
    #region
    public int GetContactWallCount()
    {
        return contactWallCount;
    }

    public void SetContactWallCount(int count)
    {
        contactWallCount = count;
    }
    public int GetContactEnemyCount()
    {
        return contactEnemyCount;
    }

    public void SetContactEnemyCount(int count)
    {
        contactEnemyCount = count;
    }

    public float GetContactWallDecreaseDamage()
    {
        return contactWallDecreaseDamage;
    }

    public void SetContactWallDecreaseDamage(float Damage)
    {
        contactWallDecreaseDamage = Damage;
    }

    public float GetContactEnemyDecreaseDamage()
    {
        return contactEnemyDecreaseDamage;
    }

    public void SetContactEnemyDecreaseDamage(float Damage)
    {
        contactEnemyDecreaseDamage = Damage;
    }

    public float GetFinalDecreaseDamage()
    {
        return finalDecreaseDamage;
    }

    public void SetFinalDecreaseDamage(float Damage)
    {
        finalDecreaseDamage = Damage;
    }

    public float GetDarkTouchDecreaseDamage()
    {
        return darkTouchDecreaseDamage;
    }

    public void SetDarkTouchDecreaseDamage(float Damage)
    {
        darkTouchDecreaseDamage = Damage;
    }


    public float GetBlazeDecresaseDamage()
    {
        return blazeDecresaseDamage;
    }

    public void SetBlazeDecresaseDamage(float Damage)
    {
        blazeDecresaseDamage = Damage;
    }

    public float GetFireOrbDecreaseDamage()
    {
        return fireOrbDecreaseDamage;
    }

    public void SetFireOrbDecreaseDamage(float Damage)
    {
        fireOrbDecreaseDamage = Damage;
    }
    public float GetFairyDecreaseDamage()
    {
        return fairyDecreaseDamage;
    }

    public void SetFairyDecreaseDamage(float Damage)
    {
        fairyDecreaseDamage = Damage;
        
    }
    public bool GetDeathBoom()
    {
        return isDeathBoom;
    }
    public void SetDeathBoom(bool isBoom)
    {
        isDeathBoom = isBoom;
    }


    #endregion


}
