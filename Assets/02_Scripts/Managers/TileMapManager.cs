using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : MonoBehaviour
{

    public Image SelectImage; // UI�� ǥ���� �̹���
    public Text SelectName; // �� �̸� ǥ��
    public Sprite[] SelectImages; // �� �̹��� �迭
    public string[] SelectNames; // �� �̸� �迭
    public GameObject[] MapPrefabs; // �� ������ �迭
    public Transform mapSpawnPoint;
    public Camera mainCamera;

    private int SelectIndex = 0; // ���� ���õ� ���� �ε���
    private GameObject currentMapInstance; // ���� Ȱ��ȭ�� �� ������

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
        // ���� �� ����
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
            Debug.LogError("�� �������� �������� ����: " + SelectNames[SelectIndex]);
        }
    }
}