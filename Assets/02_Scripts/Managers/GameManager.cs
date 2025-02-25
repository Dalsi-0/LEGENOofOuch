using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // �ϼ��Ǹ� ���� �ּ� ����
    // Player player;
    // Battel battle;
    // PlayerClassEnum chooseplayerClass;
    // StageEnum chooseStage;

    public AbilityManager AbilityManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public ProjectileManager ProjectileManager { get; private set; }
    public SelectManager SelectManager { get; private set; }
    public TileMapManager TileMapManager { get; private set; }
    public MonsterManager monsterManager;
    public Test test; 
    public Transform playerSpawn;
    public Transform[] monsterSpawn;

    public GameObject playerPrefab; 
    public PlayerCharacter player;
    public int healReward = 0;

    private void Awake()
    {


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        
        test.SpawnRandomMap();
        Debug.Log("StartGame");
        SpawnPlayer();
        SpawnMonsters();
        //������
        //�÷��̾����
        //������������
        //

    }

    public void KillMonster(EnemyCharacter enemy)
    {
        monsterManager.RemoveEnemyOnDeath(enemy);
        //ü��ȸ�� ��ų�� ������ �� ��ġ��ŭ ü���� ȸ�������ݴϴ�.
        player.ChangeHealth(healReward);
        Debug.Log("KillMonster");
    }

    public void GetTransrate(Transform _playerSpawn, Transform[] _monsterSpawn)
    {
        playerSpawn= _playerSpawn;
        monsterSpawn = _monsterSpawn;
    }

    public void SpawnPlayer()
    {
        if (playerSpawn == null)
        {
            Debug.LogError("PlayerSpawn ��ġ�� �������� �ʾҽ��ϴ�!");
            return;
        }

        if (player == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
            player = newPlayer.GetComponent<PlayerCharacter>();

            if (player != null)
            {
                Debug.Log("���ο� �÷��̾ �����Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("������ �÷��̾ PlayerCharacter ������Ʈ�� �����ϴ�!");
            }
        }
        else
        {
            player.transform.position = playerSpawn.position;
            Debug.Log("���� �÷��̾ ���� ��ġ�� �̵��Ǿ����ϴ�.");
        }
    }

    public void SpawnMonsters()
    {
        //���⿡ �������� �Ŵ������� ���� ������ �����ٰ�
        if (monsterSpawn == null || monsterSpawn.Length < 3)
        {
            Debug.LogError("���� ���� ����Ʈ�� �����մϴ�! �ּ� 3�� �̻� �ʿ��մϴ�.");
            return;
        }

        // ������ 3���� ���� ��ġ ���� (�ߺ� ����)
        Transform[] selectedSpawns = GetRandomSpawnPoints(3);

        // ���õ� ��ġ�� ���� ����
        foreach (Transform spawnPoint in selectedSpawns)
        {
            Debug.Log("���� �����Ѱ���");
            monsterManager.Spawn(spawnPoint);
        }
    }

    private Transform[] GetRandomSpawnPoints(int count)
    {
        List<Transform> spawnList = new List<Transform>(monsterSpawn);
        Transform[] selected = new Transform[count];

        // Fisher-Yates ������ ����Ͽ� ����Ʈ ����
        for (int i = spawnList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = spawnList[i];
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        // �տ������� `count`�� ����
        for (int i = 0; i < count; i++)
        {
            selected[i] = spawnList[i];
        }

        return selected;
    }
}

