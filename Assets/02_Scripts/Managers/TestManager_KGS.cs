using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager_KGS : MonoBehaviour
{
    [SerializeField] PlayerCharacter player;
    [SerializeField] MonsterManager MM;

    [SerializeField] Rect[] spawnPoints;
    EnemyCharacter enemy;

    /// <summary>
    /// ��Ŭ��:�� ����, �����̽�:�÷��̾� 2������, E:���� ���� ������ ������ 2������
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MM.Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        if (Input.GetKeyDown(KeyCode.Space))
            player.ChangeHealth(-2);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (enemy == null)
                enemy = FindFirstObjectByType<EnemyCharacter>();
            enemy?.ChangeHealth(-2);
        }
    }
}
