using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 완성되면 각각 주석 해제
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
        //적생성
        //플레이어생성
        //스테이지선택
        //

    }

    public void KillMonster()
    {
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
            Debug.LogError("PlayerSpawn 위치가 설정되지 않았습니다!");
            return;
        }

        if (player == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
            player = newPlayer.GetComponent<PlayerCharacter>();

            if (player != null)
            {
                Debug.Log("새로운 플레이어가 생성되었습니다.");
            }
            else
            {
                Debug.LogError("생성된 플레이어에 PlayerCharacter 컴포넌트가 없습니다!");
            }
        }
        else
        {
            player.transform.position = playerSpawn.position;
            Debug.Log("기존 플레이어가 스폰 위치로 이동되었습니다.");
        }
    }
    public void SpawnMonsters()
    {
        //여기에 스테이지 매니저에서 몬스터 마리수 정해줄것
        if (monsterSpawn == null || monsterSpawn.Length < 3)
        {
            Debug.LogError("몬스터 스폰 포인트가 부족합니다! 최소 3개 이상 필요합니다.");
            return;
        }

        // 랜덤한 3개의 스폰 위치 선택 (중복 없이)
        Transform[] selectedSpawns = GetRandomSpawnPoints(3);

        // 선택된 위치에 몬스터 생성
        foreach (Transform spawnPoint in selectedSpawns)
        {
            Debug.Log("몬스터 생성넘겨줌");
            monsterManager.Spawn(spawnPoint);
        }
    }

    private Transform[] GetRandomSpawnPoints(int count)
    {
        List<Transform> spawnList = new List<Transform>(monsterSpawn);
        Transform[] selected = new Transform[count];

        // Fisher-Yates 셔플을 사용하여 리스트 섞기
        for (int i = spawnList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = spawnList[i];
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        // 앞에서부터 `count`개 선택
        for (int i = 0; i < count; i++)
        {
            selected[i] = spawnList[i];
        }

        return selected;
    }
}

