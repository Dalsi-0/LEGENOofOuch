using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    SelectManager selectManager;
    GameManager gameManager;

    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject previousStageButton;

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject settingUI;
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
        }
    }


    public void NextStageButton()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();
        if (number < 2)
        {
            int nextNumber = number + 1;
            GameManager.Instance.SelectManager.SetSelectedStageIndex(nextNumber);
            selectManager.UpdateStageUI();

            previousStageButton.SetActive(true);
            if (nextNumber == 2)
            {
                nextStageButton.SetActive(false);
            }
            
        }
    }

    public void PreviousStageButton()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();
        if (number > 0)
        {
            int nextnumber = number - 1;
            GameManager.Instance.SelectManager.SetSelectedStageIndex(nextnumber);
            selectManager.UpdateStageUI();

            nextStageButton.SetActive(true);
            if (nextnumber == 0)
            {
                previousStageButton.SetActive(false);
            }
            
        }
    }



    /*
    public void NextStage()
    {
        SelectIndex = (SelectIndex + 1) % SelectImages.Length;
        UpdateStageUI();
        LoadSelectedMap();
    }

    public void PreviousStage()
    {
        SelectIndex = (SelectIndex - 1 + SelectImages.Length) % SelectImages.Length;
        UpdateStageUI();
        LoadSelectedMap();
    }*/
}
