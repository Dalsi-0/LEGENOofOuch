using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{

    [SerializeField] private Image imageStage;
    [SerializeField] private Sprite[] stageImages;
    [SerializeField] private string[] stageName;
    [SerializeField] private TextMeshProUGUI textStageName;
    private int selectedStageIndex = 0; // 선택된 스테이지 인덱스


    public Image characterPreview; //선택된 캐릭터 미리보기
    [SerializeField] private Sprite[] characterImages; //캐릭터이미지 배열
    [SerializeField] private string[] characterNames; // 캐릭터 이름
    public TextMeshProUGUI characterNameText; // 선택된 캐릭터 이름 표시
    private int selectedCharacterIndex = 0; // 선택된 캐릭터 인덱스

    private void Awake()
    {
        SelectCharater(0);
        SetSelectedStageIndex(0);
    }

    public void SelectCharater(int index)
    {
        selectedCharacterIndex = index;
        characterPreview.sprite = characterImages[index];
        characterNameText.text = characterNames[index];
    }



    public int GetSelectedStageIndex()
    {
        return selectedStageIndex;
    }

    public void SetSelectedStageIndex(int number)
    {
        selectedStageIndex = number;
        if (number < 0 || number >= stageImages.Length)
        {
            Debug.LogError($"SetSelectedStageIndex: 잘못된 인덱스 ({number})입니다. 범위를 벗어났습니다.");
            return;
        }
        imageStage.sprite = stageImages[number];
        if (imageStage == null)
        {
            Debug.LogError("SetSelectedStageIndex: imageStage가 null입니다.");
            return;
        }
        if (stageImages == null || stageImages.Length == 0)
        {
            Debug.LogError("SetSelectedStageIndex: stageImages 배열이 초기화되지 않았습니다.");
            return;
        }
        if (textStageName == null)
        {
            Debug.LogError("SetSelectedStageIndex: textStageName이 null입니다.");
            return;
        }
        if (stageName == null || stageName.Length == 0)
        {
            Debug.LogError("SetSelectedStageIndex: stageName 배열이 초기화되지 않았습니다.");
            return;
        }
        textStageName.text = stageName[number];
    }
    public void UpdateStageUI()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();
        if (stageImages.Length > 0 && stageName.Length > 0)
        {
            imageStage.sprite = stageImages[number];
            textStageName.text = stageName[number];
        }
    }


}
