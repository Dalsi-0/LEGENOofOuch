using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefebs;
    [SerializeField] GameObject[] bossPrefebs;
    public List<EnemyCharacter> spawnedEnemys = new();

    public bool ClearSpawn => spawnedEnemys.Count == 0;

    /// <summary>
    /// ȣ��Ǹ� ������ ��ġ���� ������ ���� ��Ÿ���ϴ�.
    /// </summary>
    /// <param name="rect">���� ��Ÿ�� �����Դϴ�.</param>
    public void Spawn(Transform spawnPoint)
    {
        Debug.Log("����Spawn");
        GameObject randomPrefeb = monsterPrefebs[Random.Range(0, monsterPrefebs.Length)];

        MonsterSpawn(spawnPoint, randomPrefeb);
    }

    /// <summary>
    /// �������͸� ������ ��ġ�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="spawnPoint"></param>
    public void BossSpawn(Transform spawnPoint)
    {
        Debug.Log("Boss����Spawn");
        GameObject randomPrefeb = bossPrefebs[Random.Range(0, monsterPrefebs.Length)];

        MonsterSpawn(spawnPoint, randomPrefeb);
    }

    void MonsterSpawn (Transform spawnPoint, GameObject randomPrefeb)
    {
        //��������Ʈ�� �޾Ƽ� ����
        Debug.Log($"���ͽ��� ��ġ{spawnPoint}");
        GameObject spawned = Instantiate(randomPrefeb, spawnPoint.position, spawnPoint.rotation);
        EnemyCharacter enemyCharacter = spawned.GetComponent<EnemyCharacter>();

        spawnedEnemys.Add(enemyCharacter);
    }

    /// <summary>
    /// �ʵ��� ���� �׾����� �ݿ��ϴ� �޼����Դϴ�. EnemyCharacter�� Death���� ȣ���մϴ�.
    /// </summary>
    /// <param name="enemy">���� ���Դϴ�. �� �ڱ� �ڽ��Դϴ�.</param>
    public void RemoveEnemyOnDeath(EnemyCharacter enemy)
    {
        spawnedEnemys.Remove(enemy);
    }
}
