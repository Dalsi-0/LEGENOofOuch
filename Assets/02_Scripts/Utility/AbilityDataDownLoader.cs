using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

[CustomEditor(typeof(AbilityDataDownLoader))]
public class SheetDownButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AbilityDataDownLoader fnc = (AbilityDataDownLoader)target;
        if (GUILayout.Button("Download SheetData"))
        {
            fnc.StartDownload(true);
        }
    }
}

public class AbilityDataDownLoader : MonoBehaviour
{
    [SerializeField] private AbilityRepositoy abilityRepositoy; 
    [SerializeField] private List<AbilityDataSO> abilityDataSO = new List<AbilityDataSO>();


    const string URL_AbilityDataSheet = "https://docs.google.com/spreadsheets/d/1Pl0qeIoV5spMGGxwze2p57locYqj8LpiyNRB4fx34r0/export?format=tsv&range=A1:H24"; // ability

    private void Awake()
    {
      //  StartDownload(false);
    }
    private void Start()
    {
        Invoke("SetActiveDisable", 10f);
    }
    private void SetActiveDisable()
    {
        gameObject.SetActive(false);
    }
    public void StartDownload(bool renameFiles)
    {
        StartCoroutine(DownloadAbilityData(renameFiles));
    }

    /// <summary>
    /// ���� ���������Ʈ���� �ɷ�ġ �����͸� �ٿ�ε��Ͽ� ScriptableObject�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="renameFiles">false��� so���� �̸����� X</param>
    /// <returns></returns>
    IEnumerator DownloadAbilityData(bool renameFiles)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_AbilityDataSheet);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string tsvText = www.downloadHandler.text;
            string json = ConvertTSVToJson(tsvText);

            JArray jsonData = JArray.Parse(json); // JSON ���ڿ��� JArray�� ��ȯ
            ApplyDataToSO(jsonData, renameFiles);
        }
        else
        {
            Debug.LogError("������ �������� ����: " + www.error);
        }

        // CreatePrefabs();
        ApplyAbilityDataSO();
    }

    /// <summary>
    /// TSV �����͸� JSON �������� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="tsv">TSV ������ ���ڿ�</param>
    /// <returns>��ȯ�� JSON ������ ���ڿ�</returns>
    string ConvertTSVToJson(string tsv)
    {
        string[] lines = tsv.Split('\n'); // �� ������ �и�
        if (lines.Length < 2) return "[]"; // �����Ͱ� ������ �� JSON �迭 ��ȯ

        string[] headers = lines[0].Split('\t'); // ù ��° ���� ����� ���
        JArray jsonArray = new JArray();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split('\t'); // ������ �� �и�
            JObject jsonObject = new JObject();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                string cleanValue = values[j].Trim();
                jsonObject[headers[j].Trim()] = cleanValue; // `+` ���ڵ� �� ��
            }

            jsonArray.Add(jsonObject);
        }

        return jsonArray.ToString();
    }

    /// <summary>
    /// �ٿ�ε��� �����͸� ScriptableObject(AbilityDataSO)�� �����ϴ� �Լ�.
    /// ���� SO �����͸� �����ϴ� ������ ��� ScriptableObject�� ������ �� ���ο� �����͸� �����Ͽ� �����Ѵ�.
    /// </summary>
    /// <param name="jsonData">���������Ʈ���� ���� JSON ������</param>
    /// <param name="renameFiles">true�� SO ���ϸ��� JSON �������� name ������ ����</param>
    private void ApplyDataToSO(JArray jsonData, bool renameFiles)
    {
        ClearAllAbilityDataSO();
        abilityDataSO.Clear();

        for (int i = 0; i < jsonData.Count; i++)
        {
            JObject row = (JObject)jsonData[i];

            AbilityEnum abilityEnum = Enum.TryParse(row["EnumID"]?.ToString(), out AbilityEnum parsedAbility) ? parsedAbility : default;
            string abilityName = row["name"]?.ToString() ?? "";
            string description = row["description"]?.ToString() ?? "";
            RankEnum rankEnum = Enum.TryParse(row["rank"]?.ToString(), out RankEnum parsedRank) ? parsedRank : default;
            bool isUpgraded = row["isUpgraded"]?.ToString() == "0"; // 0�̸� true, �ƴϸ� false

            float[] values = new float[2];
            values[0] = float.TryParse(row["value1"]?.ToString(), out float v1) ? v1 : 0;
            values[1] = float.TryParse(row["value2"]?.ToString(), out float v2) ? v2 : 0;

            AbilityDataSO abilityData = new AbilityDataSO();

            // ���� SO ������ �����ϸ� ���� ����
            if (i < abilityDataSO.Count)
            {
                abilityData = abilityDataSO[i];
            }
            else
            {
                Debug.Log("sefes");
                abilityData = CreateNewAbilityDataSO(abilityName); // ���ο� SO ����
                abilityDataSO.Add(abilityData);
            }

            if (renameFiles)
            {
                RenameScriptableObjectFile(abilityData, abilityName);
            }

            abilityData.SetData(abilityEnum, abilityName, description, rankEnum, isUpgraded, values);
            EditorUtility.SetDirty(abilityData); // ���� ���� ���� ����, �ݵ�� �ϳ������� ���������� �����Ű��.

            Debug.Log($"{abilityData.name} ������Ʈ �Ϸ�");
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// SO�� ���ϸ��� �����ϴ� �Լ�
    /// </summary>
    private void RenameScriptableObjectFile(AbilityDataSO so, string newFileName)
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(so);
        string newPath = Path.GetDirectoryName(path) + "/" + newFileName + ".asset";

        if (path != newPath)
        {
            AssetDatabase.RenameAsset(path, newFileName);
            Debug.Log($"���ϸ� ����: {path} => {newPath}");

            // ��� �����Ͽ� �ݿ�
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }

    /// <summary>
    /// ������ ���� ���� ��� ScriptableObject(AbilityDataSO) ������ �����ϴ� �Լ�.
    /// </summary>
    private void ClearAllAbilityDataSO()
    {
        string folderPath = "Assets/08_Data/ScriptableObjects/Abilities";

        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning("SO ������ �������� ����");
            return;
        }

        string[] files = Directory.GetFiles(folderPath, "*.asset");

        foreach (string file in files)
        {
            AssetDatabase.DeleteAsset(file);
            Debug.Log($"������: {file}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// ���ο� AbilityDataSO ScriptableObject�� �����ϰ� ������ ������ �����ϴ� �Լ�.
    /// </summary>
    /// <param name="fileName">������ SO ������ �̸�</param>
    /// <returns>������ AbilityDataSO ��ü</returns>
    private AbilityDataSO CreateNewAbilityDataSO(string fileName)
    {
        AbilityDataSO newSO = ScriptableObject.CreateInstance<AbilityDataSO>();

        string folderPath = "Assets/08_Data/ScriptableObjects/Abilities"; 
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string assetPath = $"{folderPath}/{fileName}.asset";
        AssetDatabase.CreateAsset(newSO, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"���ο� ScriptableObject ����: {assetPath}");
        return newSO;
    }

    /// <summary>
    /// ������ �ִ� SO ������ ���� ������ ���� �� �����
    /// </summary>
    private void CreatePrefabs()
    {
        string folderPath = "Assets/03_Prefabs/Abilities";

        // ������ ������ ����
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/03_Prefabs", "Abilities");
        }

        for (int i = 0; i < abilityDataSO.Count; i++)
        {
            string prefabPath = $"{folderPath}/{abilityDataSO[i].AbilityName}.prefab";

            GameObject abilityObject = new GameObject(abilityDataSO[i].AbilityName);
            abilityObject.AddComponent<AbilityController>();

            // ���� �������� �ִٸ� �����
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(abilityObject, prefabPath);

            EditorUtility.SetDirty(abilityObject); // ���� ���� ���� ����, �ݵ�� �ϳ������� ���������� �����Ű��.

            DestroyImmediate(abilityObject);
        }

        // ���� ���� ����
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void ApplyAbilityDataSO()
    {
        abilityRepositoy.SetabilityDataSOs(abilityDataSO.ToArray());
    }
}
