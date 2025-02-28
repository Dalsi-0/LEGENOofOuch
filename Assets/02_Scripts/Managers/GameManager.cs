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
    /// 게임 시작
    /// 게임 시작에 관련된 함수들 호출
    /// 맵생성과 플레이어생성 , 몬스터생성
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
        StartCoroutine(GameTimerCoroutine());  // 코루틴 시작
    }

    /// <summary>
    /// 게임 진행 타이머 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameTimerCoroutine()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(1f); // 1초마다 실행
            gameTimer += 1f; // 1초씩 증가
        }
    }

    /// <summary>
    /// 게임 종료 시 호출할 함수
    /// </summary>
    public void EndGame()
    {
        Initialized();
        bool isClear = (player != null && player.CurHp > 0);
        UIManager.GameEndUI(isClear, gameTimer);

        string sfxName = isClear ? "Clear" : "lose";
        SoundManager.instance.PlayBGM(sfxName);

        if (player != null)
            Destroy(player.gameObject);
        isGameRunning = false;

    }

    /// <summary>
    /// 몬스터를 죽였을때 호출되는 함수
    /// </summary>
    /// <param name="enemy"></param>
    public void KillMonster(EnemyCharacter enemy)
    {
        MonsterManager.RemoveEnemyOnDeath(enemy);
        //플레이어에게 경험치를 제공합니다.
        player.GetExp(17);
        //체력회복 스킬이 있으면 그 수치만큼 체력을 회복시켜줍니다.
        player.ChangeHealth(healReward);
        if (MonsterManager.ClearSpawn)
        {

            if (LevelManager.NowRound() < 14)
            { 
            PlayerPauseControll(true);
            GachaManager.StartGacha();
            }
        }
    }

    /// <summary>
    /// 가챠에서 선택한 능력을 어빌리티매니저에게 전달
    /// </summary>
    /// <param name="abilityEnum"></param>
    public void GetAbility(AbilityEnum abilityEnum)
    {
        AbilityManager.SetAbility(abilityEnum);
        Achievements.TriggerFirstAbility();
    }

    /// <summary>
    /// 어빌리티 매니저에서 스킬정보를 받아와서 UI에 전달
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
    /// DevilStage/TradeUI 거래진행
    /// </summary>
    public void Trade()
    {
        GetAbility(AbilityEnum.ExtraLife);
        player.ChangeHealth(-3f);
    }

    /// <summary>
    ///몬스터가 없으면 다음맵으로 넘어가는 충돌체 활성화
    /// </summary>
    public void GoNextMap()
    {
        if (MonsterManager.ClearSpawn)
            LevelManager.NextMap();
    }

    /// <summary>
    /// 초기화함수
    /// 어빌리티매니저 ,프로젝타일매니저,가챠매니저 초기화
    /// </summary>
    public void Initialized()
    {
        SoundManager.instance.PlayBGM("MainBGM");
        AbilityManager.ClearOwnedAbilities();
        ProjectileManager.ClearProjectile();
        GachaManager.gacha.gachaAbilityController.ClearUpgradeCount();
        LevelManager.ClearPotion();
        MonsterManager.ClearSpawns();
        AbilityManager.SetMultiShotOn(false);
        healReward = 0;
    }

    /// <summary>
    /// 플레이어 움직임을 멈추는 함수
    /// </summary>
    /// <param name="paused"></param>
    public void PlayerPauseControll(bool paused)
    {
        player.PlayerPaused = paused;
    }
}
