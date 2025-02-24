using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{
    public class CharacterSelcet : MonoBehaviour
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
            StageIndex = (StageIndex + 1) % StageImages.Length;
            UpdateStageUI();
        }

        public void PreviousStage()
        {
            StageIndex = (StageIndex - 1) % StageImages.Length;
            UpdateStageUI();
        }


        void Start()
        {
            UpdateStageUI();
        }
    }
}
