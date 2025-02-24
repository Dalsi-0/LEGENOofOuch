using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public Slider bgmSlider;

    void Start()
    {
        // 슬라이더 값 초기화 (저장된 값 불러오기)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);

        // 초기 볼륨 설정
        bgmSource.volume = bgmSlider.value;

        // 슬라이더 이벤트 연결
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); // 설정 저장
    }
}

