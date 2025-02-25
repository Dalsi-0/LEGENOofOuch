using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefebs;
    List<EnemyCharacter> spawnedEnemys = new();

    PlayerCharacter player;
    public int healReward = 0;

    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacter)).GetComponent<PlayerCharacter>();
    }

    /// <summary>
    /// ȣ��Ǹ� ������ �������� ������ ��ġ���� ������ ���� ��Ÿ���ϴ�.
    /// </summary>
    /// <param name="rect">���� ��Ÿ�� �����Դϴ�.</param>
    public void Spawn(Transform spawnPoint)
    {
        Debug.Log("����Spawn");
        GameObject randomPrefeb = monsterPrefebs[Random.Range(0, monsterPrefebs.Length)];

        //Vector2 randomP = new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
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

        //ü��ȸ�� ��ų�� ������ �� ��ġ��ŭ ü���� ȸ�������ݴϴ�.
        player.ChangeHealth(healReward);

        //���� ���� ������ ���������� Ŭ���� �� ���Դϴ�.
        if (spawnedEnemys.Count == 0)
        {
            //���������� Ŭ���� ����ϴ�.
        }
    }
}
