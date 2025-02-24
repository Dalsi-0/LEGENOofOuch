using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject[] mapPrefabs; // ���� ���� �� ������ �迭
    private GameObject selectedMapInstance; // ���õ� ���� �ν��Ͻ�
    private StageContainer stageContainer;


    public void SpawnRandomMap()
    {
        // ������ �� ����
        GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];

        // ���� ���� (0,0,0 ��ġ�� ��ġ)
        selectedMapInstance = Instantiate(selectedMap, Vector3.zero, Quaternion.identity);
        stageContainer = selectedMapInstance.GetComponent<StageContainer>();
        Debug.Log($"���õ� ��: {selectedMapInstance.name}");

        // ������ �ʿ��� �÷��̾� ���� ����Ʈ ã��
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;

        GameManager.Instance.GetTransrate(playerSpawnPoint, monsterSpawnPoint);

    }
}


