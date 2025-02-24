using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManagert : MonoBehaviour
{
    public Image characterPreview; //선택된 캐릭터 미리보기
    public Sprite[] characterImages; //캐릭터이미지 배열
    public string[] characterNames; // 캐릭터 이름
    public Text characterNameText; // 선택된 캐릭터 이름 표시

    private int selectedCharacterIndex = 0; // 선택된 캐릭터 인덱스
    void Start()
    {

        LoadSavedCharater();
    }


    public void SelectCharater(int index)
    { 
        
        selectedCharacterIndex = index;
        characterPreview.sprite = characterImages[index];
        characterNameText.text = characterNames[index];

        SaveCharacter();
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
        Debug.Log("Character Saved: " + characterNames[selectedCharacterIndex]);
    }

    void LoadSavedCharater()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        SelectCharater(selectedCharacterIndex);
    }

}
