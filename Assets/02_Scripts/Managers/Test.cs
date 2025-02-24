using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject[] mapPrefabs; // ���� ���� �� ������ �迭
    private GameObject selectedMapInstance; // ���õ� ���� �ν��Ͻ�

    private void Start()
    {
        SpawnRandomMap();
    }

    private void SpawnRandomMap()
    {
        // ������ �� ����
        GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];

        // ���� ���� (0,0,0 ��ġ�� ��ġ)
        selectedMapInstance = Instantiate(selectedMap, Vector3.zero, Quaternion.identity);

        Debug.Log($"���õ� ��: {selectedMapInstance.name}");

        // ������ �ʿ��� �÷��̾� ���� ����Ʈ ã��
        Transform playerSpawnPoint = selectedMapInstance.transform.Find("PlayerSpawnPoint");
        Transform monsterSpawnPoint = selectedMapInstance.transform.Find("MonsterSpawnPoint");

        ////if (PlayerSpawnPoint != null)
        //{
        //    //Debug.Log($"�÷��̾� ���� ��ġ: {PlayerSpawnPoint.position}");
        //    //MovePlayerToSpawn(PlayerSpawnPoint);
        ////}

    }

    private void GiveTransrate()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            Debug.Log("�÷��̾ ���� ��ġ�� �̵���.");
        }
        else
        {
            Debug.LogError("GameManager �Ǵ� �÷��̾� ������Ʈ�� �����ϴ�!");
        }
    }
}


