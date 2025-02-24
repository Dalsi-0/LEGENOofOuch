using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectManager : MonoBehaviour
{

    public Image StageImage; //스테이지 이미지 배열
    public Text StageName; // 스테이지 이름
    public Sprite[] StageImages; //스테이지 이미지 배열
    public string[] StageNames; // 스테이지 이름

    private int StageIndex = 0; // 선택된 캐릭터 인덱스



    void UpdateStageUI()
    {
        if (StageImages.Length > 0 && StageNames.Length > 0)
        {
            StageImage.sprite = StageImages[StageIndex];
            StageName.text = StageNames[StageIndex];
        }
    }

    public void NextStage()
    {
        if (StageIndex == StageImages.Length - 1) //마지막이면 처음으로
        {
            StageIndex = 0;
        }
        else
        {
            StageIndex++;
        }

        UpdateStageUI();
    }

    public void PreviousStage()
    {
        if (StageIndex == 0) //처음이면 마지막으로
        {
            StageIndex = StageImages.Length - 1;
        }
        else
        {
            StageIndex--;
        }

        UpdateStageUI();
    }

    void Start()
    {
        UpdateStageUI();
    }
}
