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

    public Transform playerspown;

    public PlayerCharacter player;
    public Transform MmonsterSpawn { get; private set; }

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
        //������
        //�÷��̾����
        //������������
        //

    }

    public void KillMonster()
    {
        Debug.Log("KillMonster");
    }
    public void GetTransrate()
    {

    }
}
