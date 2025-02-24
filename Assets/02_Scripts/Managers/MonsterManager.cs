using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefebs;
    List<EnemyCharacter> spawnedEnemys = new();

    /// <summary>
    /// ȣ��Ǹ� ������ �������� ������ ��ġ���� ������ ���� ��Ÿ���ϴ�.
    /// </summary>
    /// <param name="rect">���� ��Ÿ�� �����Դϴ�.</param>
    public void Spawn(Transform spawnPoint)
    {
        GameObject randomPrefeb = monsterPrefebs[Random.Range(0, monsterPrefebs.Length)];

        //Vector2 randomP = new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
        //��������Ʈ�� �޾Ƽ� ����

        GameObject spawned = Instantiate(randomPrefeb, new Vector3(spawnPoint.position.x, spawnPoint.position.y), Quaternion.identity);
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
        if (spawnedEnemys.Count == 0)
        {
            //���������� Ŭ���� ����ϴ�.
        }
    }
}
