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

    public Button StartButton;


    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        UpdateStageUI();
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

    public void NextStage()
    {
        SelectIndex = (SelectIndex + 1) % SelectImages.Length;
        UpdateStageUI();
        LoadSelectedMap();
    }

    public void PreviousStage()
    {
        SelectIndex = (SelectIndex - 1 + SelectImages.Length) % SelectImages.Length;
        UpdateStageUI();
        LoadSelectedMap();
    }

    void LoadSelectedMap()
    {
        // ���� �� ����
        if (currentMapInstance != null)
        {
            Destroy(currentMapInstance);
        }

        if (MapPrefabs != null && SelectIndex < MapPrefabs.Length && MapPrefabs[SelectIndex] != null)
        {
            currentMapInstance = Instantiate(MapPrefabs[SelectIndex], new Vector3(-20, 0, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("�� �������� �������� ����: " + SelectNames[SelectIndex]);
        }
    }
    void StartGame()
    {
        if(currentMapInstance != null)
        {
            mainCamera.transform.position = new Vector3(currentMapInstance.transform.position.x, currentMapInstance.transform.position.y, mainCamera.transform.position.z);
            Debug.Log("������ �̵�");
        }
        else
        {
            Debug.LogError("���� ����");
        }
        

    }
}