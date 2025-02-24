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
        // �����̴� �� �ʱ�ȭ (����� �� �ҷ�����)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);

        // �ʱ� ���� ����
        bgmSource.volume = bgmSlider.value;

        // �����̴� �̺�Ʈ ����
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); // ���� ����
    }
}

