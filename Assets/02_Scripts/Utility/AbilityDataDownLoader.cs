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


    const string URL_AbilityDataSheet = "https://docs.google.com/spreadsheets/d/1Pl0qeIoV5spMGGxwze2p57locYqj8LpiyNRB4fx34r0/export?format=tsv&range=A1:H32"; // ability

    private void Awake()
    {
      //  StartDownload(false);
    }
    private void Start()
    {
    //    Invoke("SetActiveDisable", 10f);
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
    /// 구글 스프레드시트에서 능력치 데이터를 다운로드하여 ScriptableObject에 적용하는 함수
    /// </summary>
    /// <param name="renameFiles">false라면 so파일 이름변경 X</param>
    /// <returns></returns>
    IEnumerator DownloadAbilityData(bool renameFiles)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_AbilityDataSheet);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string tsvText = www.downloadHandler.text;
            string json = ConvertTSVToJson(tsvText);

            JArray jsonData = JArray.Parse(json); // JSON 문자열을 JArray로 변환
            ApplyDataToSO(jsonData, renameFiles);
        }
        else
        {
            Debug.LogError("데이터 가져오기 실패: " + www.error);
        }

        CreatePrefabs();
        ApplyAbilityDataSO();
    }

    /// <summary>
    /// TSV 데이터를 JSON 형식으로 변환하는 함수
    /// </summary>
    /// <param name="tsv">TSV 형식의 문자열</param>
    /// <returns>변환된 JSON 형식의 문자열</returns>
    string ConvertTSVToJson(string tsv)
    {
        string[] lines = tsv.Split('\n'); // 줄 단위로 분리
        if (lines.Length < 2) return "[]"; // 데이터가 없으면 빈 JSON 배열 반환

        string[] headers = lines[0].Split('\t'); // 첫 번째 줄을 헤더로 사용
        JArray jsonArray = new JArray();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split('\t'); // 데이터 값 분리
            JObject jsonObject = new JObject();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                string cleanValue = values[j].Trim();
                jsonObject[headers[j].Trim()] = cleanValue; // `+` 인코딩 안 함
            }

            jsonArray.Add(jsonObject);
        }

        return jsonArray.ToString();
    }

    /// <summary>
    /// 다운로드한 데이터를 ScriptableObject(AbilityDataSO)에 적용하는 함수.
    /// 기존 SO 데이터를 보관하는 폴더의 모든 ScriptableObject를 삭제한 후 새로운 데이터를 생성하여 적용한다.
    /// </summary>
    /// <param name="jsonData">스프레드시트에서 받은 JSON 데이터</param>
    /// <param name="renameFiles">true면 SO 파일명을 JSON 데이터의 name 값으로 변경</param>
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
            bool isUpgraded = row["isUpgraded"]?.ToString() == "0"; // 0이면 true, 아니면 false

            float[] values = new float[2];
            values[0] = float.TryParse(row["value1"]?.ToString(), out float v1) ? v1 : 0;
            values[1] = float.TryParse(row["value2"]?.ToString(), out float v2) ? v2 : 0;

            AbilityDataSO abilityData = new AbilityDataSO();

            // 기존 SO 개수가 부족하면 새로 생성
            if (i < abilityDataSO.Count)
            {
                abilityData = abilityDataSO[i];
            }
            else
            {
                Debug.Log("sefes");
                abilityData = CreateNewAbilityDataSO(abilityName); // 새로운 SO 생성
                abilityDataSO.Add(abilityData);
            }

            if (renameFiles)
            {
                RenameScriptableObjectFile(abilityData, abilityName);
            }

            abilityData.SetData(abilityEnum, abilityName, description, rankEnum, isUpgraded, values);
            EditorUtility.SetDirty(abilityData); // 변경 사항 강제 적용, 반드시 하나했을때 개별적으로 적용시키기.

            Debug.Log($"{abilityData.name} 업데이트 완료");
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// SO의 파일명을 변경하는 함수
    /// </summary>
    private void RenameScriptableObjectFile(AbilityDataSO so, string newFileName)
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(so);
        string newPath = Path.GetDirectoryName(path) + "/" + newFileName + ".asset";

        if (path != newPath)
        {
            AssetDatabase.RenameAsset(path, newFileName);
            Debug.Log($"파일명 변경: {path} => {newPath}");

            // 즉시 저장하여 반영
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }

    /// <summary>
    /// 지정된 폴더 내의 모든 ScriptableObject(AbilityDataSO) 파일을 삭제하는 함수.
    /// </summary>
    private void ClearAllAbilityDataSO()
    {
        string folderPath = "Assets/08_Data/ScriptableObjects/Abilities";

        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning("SO 폴더가 존재하지 않음");
            return;
        }

        string[] files = Directory.GetFiles(folderPath, "*.asset");

        foreach (string file in files)
        {
            AssetDatabase.DeleteAsset(file);
            Debug.Log($"삭제됨: {file}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 새로운 AbilityDataSO ScriptableObject를 생성하고 지정된 폴더에 저장하는 함수.
    /// </summary>
    /// <param name="fileName">생성할 SO 파일의 이름</param>
    /// <returns>생성된 AbilityDataSO 객체</returns>
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

        Debug.Log($"새로운 ScriptableObject 생성: {assetPath}");
        return newSO;
    }

    /// <summary>
    /// 가지고 있는 SO 정보를 토대로 프리팹 생성 및 덮어쓰기
    /// </summary>
    private void CreatePrefabs()
    {
        string folderPath = "Assets/03_Prefabs/Abilities";

        // 폴더가 없으면 생성
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/03_Prefabs", "Abilities");
        }

        for (int i = 0; i < abilityDataSO.Count; i++)
        {
            string prefabPath = $"{folderPath}/{abilityDataSO[i].AbilityName}.prefab";

            GameObject abilityObject = new GameObject(abilityDataSO[i].AbilityName);
            abilityObject.AddComponent<AbilityController>();

            // 기존 프리팹이 있다면 덮어쓰기
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(abilityObject, prefabPath);

            EditorUtility.SetDirty(abilityObject); // 변경 사항 강제 적용, 반드시 하나했을때 개별적으로 적용시키기.

            DestroyImmediate(abilityObject);
        }

        // 변경 사항 저장
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void ApplyAbilityDataSO()
    {
        abilityRepositoy.SetabilityDataSOs(abilityDataSO.ToArray());
    }
}
