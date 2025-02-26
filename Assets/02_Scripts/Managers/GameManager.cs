using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // �ϼ��Ǹ� ���� �ּ� ����
    // Player player;
    // Battel battle;
    // PlayerClassEnum chooseplayerClass;
    // StageEnum chooseStage;

    [field: SerializeField] public AbilityManager AbilityManager { get; private set; }
    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] public SelectManager SelectManager { get; private set; }
    [field: SerializeField] public TileMapManager TileMapManager { get; private set; }
    [field: SerializeField] public MonsterManager MonsterManager { get; private set; }
    [field: SerializeField] public GachaManager GachaManager { get; private set; }
    public Test test; 
    //public Transform playerSpawn;
    //public Transform[] monsterSpawn;

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
        //StartGame();
    }
    /// <summary>
    /// ���� ����
    /// ���� ���ۿ� ���õ� �Լ��� ȣ��
    /// �ʻ����� �÷��̾���� , ���ͻ���
    /// </summary>
    public void StartGame()
    {

        TileMapManager.SpawnRandomMap();
        TileMapManager.MapStart();
        TileMapManager.SpawnEntity();
        Debug.Log("StartGame");
        //SpawnPlayer();
        //SpawnMonsters();
        //������
        //�÷��̾����
        //������������
        //

    }
    /// <summary>
    /// ���͸� �׿����� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="enemy"></param>
    public void KillMonster(EnemyCharacter enemy)
    {
        MonsterManager.RemoveEnemyOnDeath(enemy);
        //�÷��̾�� ����ġ�� �����մϴ�.
        player.GetExp(17);
        //ü��ȸ�� ��ų�� ������ �� ��ġ��ŭ ü���� ȸ�������ݴϴ�.
        player.ChangeHealth(healReward);
        Debug.Log("KillMonster");
    }

    ///// <summary>
    ///// ��ȯ����Ʈ�� �޾ƿ��� �Լ�
    ///// </summary>
    ///// <param name="_playerSpawn"></param>
    ///// <param name="_monsterSpawn"></param>
    //public void GetTransrate(Transform _playerSpawn, Transform[] _monsterSpawn)
    //{
    //    playerSpawn= _playerSpawn;
    //    monsterSpawn = _monsterSpawn;
    //}

    ///// <summary>
    ///// �÷��̾ ����, �̵���Ű�� �Լ�
    ///// �� �̵��� �÷��̾� ��ġ�� �ٲپ��ش�.
    ///// </summary>
    //public void SpawnPlayer()
    //{
    //    if (playerSpawn == null)
    //    {
    //        Debug.LogError("PlayerSpawn ��ġ�� �������� �ʾҽ��ϴ�!");
    //        return;
    //    }

    //    if (player == null)
    //    {
    //        GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
    //        player = newPlayer.GetComponent<PlayerCharacter>();
            

    //        if (player != null)
    //        {
    //            Debug.Log("���ο� �÷��̾ �����Ǿ����ϴ�.");
    //        }
    //        else
    //        {
    //            Debug.LogError("������ �÷��̾ PlayerCharacter ������Ʈ�� �����ϴ�!");
    //        }
    //    }
    //    else
    //    {
    //        player.transform.position = playerSpawn.position;
    //    }
    //}

    ///// <summary>
    ///// ���͸� �����ϴ� �Լ�
    ///// ��������Ʈ�� ����������Ʈ�� ���� ����
    ///// </summary>
    //public void SpawnMonsters()
    //{
    //    //���⿡ �������� �Ŵ������� ���� ������ �����ٰ�
    //    if (monsterSpawn == null || monsterSpawn.Length < 3)
    //    {
    //        Debug.LogError("���� ���� ����Ʈ�� �����մϴ�! �ּ� 3�� �̻� �ʿ��մϴ�.");
    //        return;
    //    }

    //    // ������ 3���� ���� ��ġ ���� (�ߺ� ����)
    //    Transform[] selectedSpawns = GetRandomSpawnPoints(3);

    //    // ���õ� ��ġ�� ���� ����
    //    foreach (Transform spawnPoint in selectedSpawns)
    //    {
    //        Debug.Log("���� �����Ѱ���");
    //        MonsterManager.Spawn(spawnPoint);
    //    }
    //}

    ///// <summary>
    ///// ���� ���� ����Ʈ �� ������ ����Ʈ�� �����Ͽ� ��ȯ
    ///// </summary>
    ///// <param name="count"></param>
    ///// <returns></returns>
    //private Transform[] GetRandomSpawnPoints(int count)
    //{
    //    List<Transform> spawnList = new List<Transform>(monsterSpawn);
    //    Transform[] selected = new Transform[count];

    //    // Fisher-Yates ������ ����Ͽ� ����Ʈ ����
    //    for (int i = spawnList.Count - 1; i > 0; i--)
    //    {
    //        int randomIndex = Random.Range(0, i + 1);
    //        Transform temp = spawnList[i];
    //        spawnList[i] = spawnList[randomIndex];
    //        spawnList[randomIndex] = temp;
    //    }

    //    // �տ������� `count`�� ����
    //    for (int i = 0; i < count; i++)
    //    {
    //        selected[i] = spawnList[i];
    //    }

    //    return selected;
    //}

    /// <summary>
    /// ��í���� ������ �ɷ��� �����Ƽ�Ŵ������� ����
    /// </summary>
    /// <param name="abilityEnum"></param>
    public void GetAbility(AbilityEnum abilityEnum)
    {
        AbilityManager.SetAbility(abilityEnum);
    }

    /// <summary>
    /// �����Ƽ �Ŵ������� ��ų������ �޾ƿͼ� UI�� ����
    /// </summary>
    public void SetAbilityText()
    {
        AbilityEnum[] selectedAbility = GachaManager.gacha.GetSelectedAbility();
        string[] abilityName = new string[3];
        string[] abilityDescription = new string[3];
        
        for (int i=0;i<selectedAbility.Length;i++)
        {   
            
            AbilityDataSO abilityData = AbilityManager.FindAbilityData(selectedAbility[i]);
            abilityName[i] = abilityData.AbilityName;
            int upgradeCount = GachaManager.gacha.gachaAbilityController.GetUpgradeCount(selectedAbility[i]);
            if (upgradeCount > 0)
            {
                abilityName[i] += $"\n<color=yellow>+{upgradeCount}</color>";
            }
            abilityDescription[i]= abilityData.Description.Replace("{0}", abilityData.Values[upgradeCount].ToString());
        }
        GachaManager.GetAbilityName(abilityName);
        GachaManager.GetAbilitydescription(abilityDescription);
    }
}

