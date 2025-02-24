using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectManager : MonoBehaviour
{

    public Image StageImage; //�������� �̹��� �迭
    public Text StageName; // �������� �̸�
    public Sprite[] StageImages; //�������� �̹��� �迭
    public string[] StageNames; // �������� �̸�

    private int StageIndex = 0; // ���õ� ĳ���� �ε���



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
        if (StageIndex == StageImages.Length - 1) //�������̸� ó������
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
        if (StageIndex == 0) //ó���̸� ����������
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
