using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefebs;
    List<EnemyCharacter> spawnedEnemys = new();

    /// <summary>
    /// 호출되면 지정된 범위내의 랜덤한 위치에서 무작위 적이 나타납니다.
    /// </summary>
    /// <param name="rect">적이 나타날 범위입니다.</param>
    public void Spawn(Transform spawnPoint)
    {
        Debug.Log("몬스터Spawn");
        GameObject randomPrefeb = monsterPrefebs[Random.Range(0, monsterPrefebs.Length)];

        //Vector2 randomP = new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
        //스폰포인트를 받아서 스폰
        Debug.Log($"몬스터스폰 위치{spawnPoint}");
        GameObject spawned = Instantiate(randomPrefeb, spawnPoint.position, spawnPoint.rotation);
        EnemyCharacter enemyCharacter = spawned.GetComponent<EnemyCharacter>();

        spawnedEnemys.Add(enemyCharacter);
    }

    /// <summary>
    /// 필드의 적이 죽었음을 반영하는 메서드입니다. EnemyCharacter의 Death에서 호출합니다.
    /// </summary>
    /// <param name="enemy">죽은 적입니다. 즉 자기 자신입니다.</param>
    public void RemoveEnemyOnDeath(EnemyCharacter enemy)
    {
        spawnedEnemys.Remove(enemy);
        if (spawnedEnemys.Count == 0)
        {
            //스테이지가 클리어 됬습니다.
        }
    }
}
