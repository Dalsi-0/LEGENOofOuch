using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [field: SerializeField] public AbilityManager AbilityManager { get; private set; }
    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] public SelectManager SelectManager { get; private set; }
    [field: SerializeField] public LevelManager LevelManager { get; private set; }
    [field: SerializeField] public MonsterManager MonsterManager { get; private set; }
    [field: SerializeField] public GachaManager GachaManager { get; private set; }

    public PlayerClassEnum playerClassEnum;
    public StageEnum stageEnum;
    public GameObject playerPrefab;
    public PlayerCharacter player;
    public int healReward = 0;
    public float gameTimer = 0;
    private bool isGameRunning = false;

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
        Initialized();
        playerClassEnum = SelectManager.GetSelectedCharacter();
        stageEnum = SelectManager.GetSelectedStageIndex();
        LevelManager.SpawnRandomMap(stageEnum);
        LevelManager.MapStart();
        LevelManager.SpawnEntity(playerClassEnum);

        gameTimer = 0f;
        isGameRunning = true;
        StartCoroutine(GameTimerCoroutine());  // �ڷ�ƾ ����
    }

    /// <summary>
    /// ���� ���� Ÿ�̸� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameTimerCoroutine()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(1f); // 1�ʸ��� ����
            gameTimer += 1f; // 1�ʾ� ����
        }
    }

    /// <summary>
    /// ���� ���� �� ȣ���� �Լ�
    /// </summary>
    public void EndGame()
    {
        bool isClear = (player != null && player.CurHp > 0);
        UIManager.GameEndUI(isClear, gameTimer);

        if (player != null)
            Destroy(player.gameObject);
        MonsterManager.ClearSpawns();
        isGameRunning = false;
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
            PlayerPauseControll(true);
            GachaManager.StartGacha();
        }
    }

    /// <summary>
    /// ��í���� ������ �ɷ��� �����Ƽ�Ŵ������� ����
    /// </summary>
    /// <param name="abilityEnum"></param>
    public void GetAbility(AbilityEnum abilityEnum)
    {
        AbilityManager.SetAbility(abilityEnum);
        Achievements.TriggerFirstAbility();
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
                abilityName[i] += $"+{upgradeCount}";
            }
            abilityDescription[i] = abilityData.Description.Replace("{0}", abilityData.Values[upgradeCount].ToString());
        }
        GachaManager.GetAbilityName(abilityName);
        GachaManager.GetAbilitydescription(abilityDescription);
    }

    /// <summary>
    /// DevilStage/TradeUI �ŷ�����
    /// </summary>
    public void Trade()
    {
        GetAbility(AbilityEnum.ExtraLife);
        player.ChangeHealth(-3f);
    }

    /// <summary>
    ///���Ͱ� ������ ���������� �Ѿ�� �浹ü Ȱ��ȭ
    /// </summary>
    public void GoNextMap()
    {
        if (MonsterManager.ClearSpawn)
            LevelManager.NextMap();
    }

    /// <summary>
    /// �ʱ�ȭ�Լ�
    /// �����Ƽ�Ŵ��� ,������Ÿ�ϸŴ���,��í�Ŵ��� �ʱ�ȭ
    /// </summary>
    public void Initialized()
    {
        AbilityManager.ClearOwnedAbilities();
        ProjectileManager.ClearProjectile();
        GachaManager.gacha.gachaAbilityController.ClearUpgradeCount();
    }

    /// <summary>
    /// �÷��̾� �������� ���ߴ� �Լ�
    /// </summary>
    /// <param name="paused"></param>
    public void PlayerPauseControll(bool paused)
    {
        player.PlayerPaused = paused;
    }
}
