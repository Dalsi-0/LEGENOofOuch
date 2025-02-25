using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : MonoBehaviour
{

    public Image SelectImage; // UI에 표시할 이미지
    public Text SelectName; // 맵 이름 표시
    public Sprite[] SelectImages; // 맵 이미지 배열
    public string[] SelectNames; // 맵 이름 배열
    public GameObject[] MapPrefabs; // 맵 프리팹 배열
    public Transform mapSpawnPoint;
    public Camera mainCamera;

    private int SelectIndex = 0; // 현재 선택된 맵의 인덱스
    private GameObject currentMapInstance; // 현재 활성화된 맵 프리팹

    void Start()
    {
        LoadSelectedMap();
    }

    void UpdateStageUI()
    {
        if (SelectImages.Length > 0 && SelectNames.Length > 0)
        {
            SelectImage.sprite = SelectImages[SelectIndex];
            SelectName.text = SelectNames[SelectIndex];
        }
    }

    //void GameStart()

    void LoadSelectedMap()
    {
        // 기존 맵 삭제
        if (currentMapInstance != null)
        {
            Destroy(currentMapInstance);
        }

        if (MapPrefabs != null && SelectIndex < MapPrefabs.Length && MapPrefabs[SelectIndex] != null)
        {
            currentMapInstance = Instantiate(MapPrefabs[SelectIndex], Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("맵 프리팹이 존재하지 않음: " + SelectNames[SelectIndex]);
        }
    }
}