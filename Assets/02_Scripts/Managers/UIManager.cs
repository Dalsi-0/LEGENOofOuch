using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    SelectManager selectManager;
    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject previousStageButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject GameStartButton;

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject MainCanvus;


    /// <summary>
    /// ��ưȰ��ȭ ,��Ȱ��ȭ�� ������Ʈ�Ͽ� ��� Ȯ��
    /// </summary>
    private void Update()
    {
        ButtonActivate();
    }


    public void toglePanel(int selectPanelNumber)
    {
        switch (selectPanelNumber)
        {
            case 0:
                startUI.SetActive(!startUI.activeSelf);
                break;

            case 1:
                characterUI.SetActive(!characterUI.activeSelf);
                break;

            case 2:
                settingUI.SetActive(!settingUI.activeSelf);
                break;
            case 3:
                MainCanvus.SetActive(!MainCanvus.activeSelf);

        }
    }
    /// <summary>
    /// ��ư Ȱ��ȭ , ��Ȱ��ȭ
    /// </summary>
    public void ButtonActivate()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();

        previousStageButton.SetActive(number > 0);

        nextStageButton.SetActive(number < GameManager.Instance.SelectManager.stageImages.Length - 1);


    }
    /// <summary>
    /// ������������ ȭ���� �� �� �ְ� ���ִ� ��ư
    /// </summary>
    public void NextStageButton()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();

        number = number + 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }
    /// <summary>
    /// ������������ ȭ���� �� �� �ְ� ���ִ� ��ư
    /// </summary>
    public void PreviousStageButton()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number - 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }

    public void StartButton()
    {
        
    }

}
