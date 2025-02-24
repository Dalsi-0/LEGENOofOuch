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
    public SelectManager selectManager { get; private set; }

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

    public void StartGame()
    {
        Debug.Log("StartGame");
        //적생성
        //플레이어생성
        //스테이지선택

    }

    public void KillMonster()
    {
        Debug.Log("KillMonster");
    }
}
