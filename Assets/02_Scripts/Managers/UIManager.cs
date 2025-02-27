using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    SelectManager selectManager;
    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject previousStageButton;
    [SerializeField] private GameObject startButton;

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject TutorialUI;
    [SerializeField] private GameObject AchievementUI;
    [SerializeField] private GameObject MainCanvas;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private TextMeshProUGUI GameCrealorOverText;

    private void Update()
    {
        ButtonActivate();
    }

    ///int ���� ���� �г� ����
    public void toglePanel(int selectPanelNumber)
    {
        switch (selectPanelNumber)
        {
            case 0://���� �г�
                startUI.SetActive(!startUI.activeSelf);
                break;

            case 1: //ĳ���� �г�
                characterUI.SetActive(!characterUI.activeSelf);
                break;

            case 2: //���� �г�
                settingUI.SetActive(!settingUI.activeSelf);
                break;
            case 3: //���ӽ��� ��ư
                StageEnum selectedStageIndex = GameManager.Instance.SelectManager.GetSelectedStageIndex();
                if (Enum.IsDefined(typeof(StageEnum), selectedStageIndex))
                {
                    StageEnum stage = selectedStageIndex;
                    switch (stage)
                    {
                        case StageEnum.Castle:
                            break;
                        case StageEnum.Swamp:
                            break;
                        case StageEnum.Volcano:
                            break;
                    }
                    MainCanvas.SetActive(!MainCanvas.activeSelf);
                    GameManager.Instance.StartGame();
                }
                break;
            case 4: //���ӿ��� �г� ->  ����ĵ������ �̵�
                GameManager.Instance.LevelManager.DestroyMap();
                SoundManager.instance.PlayBGM("MainBGM");
                GameOverPanel.SetActive(!GameOverPanel.activeSelf);
                MainCanvas.SetActive(!MainCanvas.activeSelf);
                break;
            case 5: //���� �г� -> �ٽý����ϴ� ��ư
                GameManager.Instance.LevelManager.DestroyMap();
                GameOverPanel.SetActive(!GameOverPanel.activeSelf);
                GameManager.Instance.StartGame();
                break;
            case 6: //Ʃ�丮�� �г�
                TutorialUI.SetActive(!TutorialUI.activeSelf);
                break;
            case 7: //���� �г�
                AchievementUI.SetActive(!AchievementUI.activeSelf);
                break;



        }
    }
    /// <summary>
    /// ��ư Ȱ��ȭ , ��Ȱ��ȭ
    /// </summary>
    public void ButtonActivate()
    {
        StageEnum number = GameManager.Instance.SelectManager.GetSelectedStageIndex();

        previousStageButton.SetActive(number > 0);

        nextStageButton.SetActive((int)number < GameManager.Instance.SelectManager.stageImages.Length - 1);
    }
    /// <summary>
    /// ����â���� �������������� ����ִ� ��ư
    /// </summary>
    public void NextStageButton()
    {
        int number = (int)GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number + 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }
    /// <summary>
    /// ����â���� ������������ ������ִ� ��ư
    /// </summary>
    public void PreviousStageButton()
    {
        int number = (int)GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number - 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }

    /// <summary>
    /// ���ӿ��� , Ŭ����� �ý�Ʈ ���
    /// </summary>
    
    public void GameEndUI(bool isClear, float gameTimer)
    {
        GameCrealorOverText.text = isClear ? "Game Clear" : "Game Over";
        GameOverPanel.SetActive(true);

        int minutes = (int)(GameManager.Instance.gameTimer / 60);
        int seconds = (int)(GameManager.Instance.gameTimer % 60);
        Time.text = $"{minutes:00}:{seconds:00}";
    }
}
