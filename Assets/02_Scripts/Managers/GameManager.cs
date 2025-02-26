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

    public PlayerClassEnum playerClassEnum;
    public StageEnum stageEnum;
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
        Initialized();
    }
    /// <summary>
    /// ���� ����
    /// ���� ���ۿ� ���õ� �Լ��� ȣ��
    /// �ʻ����� �÷��̾���� , ���ͻ���
    /// </summary>
    public void StartGame()
    {
        
        playerClassEnum = SelectManager.GetSelectedCharacter();
        stageEnum = SelectManager.GetSelectedStageIndex();
        TileMapManager.SpawnRandomMap(stageEnum);
        TileMapManager.MapStart();
        TileMapManager.SpawnEntity(playerClassEnum);
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
        if (MonsterManager.ClearSpawn)
        {
            //GachaManager.StartGacha();
        }
    }

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

        for (int i = 0; i < selectedAbility.Length; i++)
        {

            AbilityDataSO abilityData = AbilityManager.FindAbilityData(selectedAbility[i]);
            abilityName[i] = abilityData.AbilityName;
            int upgradeCount = GachaManager.gacha.gachaAbilityController.GetUpgradeCount(selectedAbility[i]);
            if (upgradeCount > 0)
            {
                abilityName[i] += $"\n<color=yellow>+{upgradeCount}</color>";
            }
            abilityDescription[i] = abilityData.Description.Replace("{0}", abilityData.Values[upgradeCount].ToString());
        }
        GachaManager.GetAbilityName(abilityName);
        GachaManager.GetAbilitydescription(abilityDescription);
    }
    public void GoNextMap()
    {
        if (MonsterManager.ClearSpawn)    
            TileMapManager.NextMap();
    }

    public void Initialized()
    {
        //������Ÿ�� �Ŵ��� init
        AbilityManager.ClearOwnedAbilities();
        ProjectileManager.ClearProjectile();
        GachaManager.gacha.gachaAbilityController.ClearUpgradeCount();
        //�÷��̾� init
        player.ClearPlayerBuf();
    }
}
