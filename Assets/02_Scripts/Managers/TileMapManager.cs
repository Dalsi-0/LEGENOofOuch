using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : MonoBehaviour
{

    public Image SelectImage; // UIø° «•Ω√«“ ¿ÃπÃ¡ˆ
    public Text SelectName; // ∏  ¿Ã∏ß «•Ω√
    public Sprite[] SelectImages; // ∏  ¿ÃπÃ¡ˆ πËø≠
    public string[] SelectNames; // ∏  ¿Ã∏ß πËø≠
    public GameObject[] MapPrefabs; // ∏  «¡∏Æ∆’ πËø≠
    public Transform mapSpawnPoint;
    public Camera mainCamera;

    private int SelectIndex = 0; // «ˆ¿Á º±≈√µ» ∏ ¿« ¿Œµ¶Ω∫
    private GameObject currentMapInstance; // «ˆ¿Á »∞º∫»≠µ» ∏  «¡∏Æ∆’

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
        // ±‚¡∏ ∏  ªË¡¶
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
            Debug.LogError("∏  «¡∏Æ∆’¿Ã ¡∏¿Á«œ¡ˆ æ ¿Ω: " + SelectNames[SelectIndex]);
        }
    }
    void StartGame()
    {
        if(currentMapInstance != null)
        {
            mainCamera.transform.position = new Vector3(currentMapInstance.transform.position.x, currentMapInstance.transform.position.y, mainCamera.transform.position.z);
            Debug.Log("∏ ¿∏∑Œ ¿Ãµø");
        }
        else
        {
            Debug.LogError("∏ ¿Ã æ¯¿Ω");
        }
        

    }
}