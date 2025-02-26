using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : MonoBehaviour
{

    public GameObject[] stageCastlePrefabs;
    public GameObject stagecastleBossPrefabs;
    public GameObject stagecastleDevilPrefabs;
    public GameObject[] stageSwampPrefabs;
    public GameObject stageSwampBossPrefabs;
    public GameObject stageSwampDevilPrefabs;
    public GameObject[] stageVolcanicPrefabs;
    public GameObject stageVolcanicBossPrefabs;
    public GameObject stageVolcanicDevilPrefabs;
   [SerializeField] private GameObject[] selectedMapInstance = new GameObject[TotalMaps];// ���õ� ���� �ν��Ͻ�
    private StageContainer stageContainer;
    private const int TotalMaps = 15;
    private const int NormalMaps = 14;
    private const int devilround = 4;
    public int roundIndex = 0;

    /// <summary>
    /// ���������� �ش��ϴ� ���� ���� 
    /// </summary>
    public void SpawnRandomMap()
    {
        //StageEnum stage = GameManager.Instance.GetStage();
        StageEnum stage = StageEnum.Castle;
        // ������ �� ����
        switch (stage)
        {
            case StageEnum.Castle:
                InstantiateMaps(stageCastlePrefabs,stagecastleDevilPrefabs,stagecastleBossPrefabs);
                break;
            case StageEnum.Swamp:
                InstantiateMaps(stageSwampPrefabs,stageSwampDevilPrefabs,stageSwampBossPrefabs);
                break;
            case StageEnum.Volcano:
                InstantiateMaps(stageVolcanicPrefabs,stageVolcanicDevilPrefabs,stageVolcanicBossPrefabs);
                break;
        }
        MapStart();
        // ������ �ʿ��� �÷��̾� ���� ����Ʈ ã��
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;

        GameManager.Instance.GetTransrate(playerSpawnPoint, monsterSpawnPoint);
    }

    /// <summary>
    /// ���� �����Ͽ� �迭�� ����ִ´�.
    /// </summary>
    /// <param name="mapPrefabs"></param>
    private void InstantiateMaps(GameObject[] mapPrefabs, GameObject devilPrefab, GameObject bossPrefab)
    {
        for (int i = 0; i < NormalMaps; i++)
        {
            if (i == devilround)
            {
                selectedMapInstance[i] = Instantiate(devilPrefab, Vector3.zero, Quaternion.identity);
                selectedMapInstance[i].SetActive(false);
                continue;
            }
            GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];
            selectedMapInstance[i] = Instantiate(selectedMap, Vector3.zero, Quaternion.identity);
            selectedMapInstance[i].SetActive(false);
        }
        selectedMapInstance[TotalMaps-1] = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        selectedMapInstance[TotalMaps - 1].SetActive(false);
    }

    public void MapStart()
    {
        selectedMapInstance[0].SetActive(true);
        Debug.Log($"���õ� ��: {selectedMapInstance[0].name}");
        SetTransrate();
    }
    /// <summary>
    /// Ŭ����� ������ Ȱ��ȭ.
    /// ���� Ȱ��ȭ�ϸ鼭 ��������Ʈ ����
    /// �������� Ŭ����� ��� �� ����
    /// </summary>
    public void NextMap()
    {
        // ���� �� ��Ȱ��ȭ
        if (roundIndex >= 0 && roundIndex < selectedMapInstance.Length)
        {
            selectedMapInstance[roundIndex].SetActive(false);
        }

        roundIndex++;

        // ���� ������ �ʿ� �����ϸ� ��� ���� �����ϰ� �������� Ŭ����
        if (roundIndex >= selectedMapInstance.Length)
        {
            foreach (GameObject map in selectedMapInstance)
            {
                Destroy(map);
            }
            // �������� Ŭ����
            return;
        }

        // ���ο� �� Ȱ��ȭ
        if (roundIndex < selectedMapInstance.Length)
        {
            selectedMapInstance[roundIndex].SetActive(true);
            SetTransrate();
        }
    }


    /// <summary>
    /// ���� �÷��̾�� ������ ���� ����Ʈ�� ���Ӹ޴������� ����
    /// </summary>
    public void SetTransrate()
    {
        stageContainer = selectedMapInstance[roundIndex].GetComponent<StageContainer>();
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;
        GameManager.Instance.GetTransrate(playerSpawnPoint, monsterSpawnPoint);
    }
}


/////////////////////////*�±Դ� �ڵ�*/////////////////////////
//public Image SelectImage; // UI�� ǥ���� �̹���
//public Text SelectName; // �� �̸� ǥ��
//public Sprite[] SelectImages; // �� �̹��� �迭
//public string[] SelectNames; // �� �̸� �迭
//public GameObject[] MapPrefabs; // �� ������ �迭
//public Transform mapSpawnPoint;
//public Camera mainCamera;

//private int SelectIndex = 0; // ���� ���õ� ���� �ε���
//private GameObject currentMapInstance; // ���� Ȱ��ȭ�� �� ������

//void Start()
//{
//    LoadSelectedMap();
//}

//void UpdateStageUI()
//{
//    if (SelectImages.Length > 0 && SelectNames.Length > 0)
//    {
//        SelectImage.sprite = SelectImages[SelectIndex];
//        SelectName.text = SelectNames[SelectIndex];
//    }
//}

////void GameStart()

//void LoadSelectedMap()
//{
//    // ���� �� ����
//    if (currentMapInstance != null)
//    {
//        Destroy(currentMapInstance);
//    }

//    if (MapPrefabs != null && SelectIndex < MapPrefabs.Length && MapPrefabs[SelectIndex] != null)
//    {
//        currentMapInstance = Instantiate(MapPrefabs[SelectIndex], Vector3.zero, Quaternion.identity);
//    }
//    else
//    {
//        Debug.LogError("�� �������� �������� ����: " + SelectNames[SelectIndex]);
//    }
//}
/////////////////////////*�±Դ� �ڵ�*/////////////////////////