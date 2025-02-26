using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips; // ����� Ŭ�� �迭

    [Header("Object Pool Settings")]
    [SerializeField] private int poolSize = 30;           // Ǯ ũ��

    private Dictionary<string, AudioClip> soundDict;      // SFX�� BGM�� ������ Dictionary
    private Queue<AudioSource> audioSourcePool;           // ������Ʈ Ǯ

    private AudioSource bgmPlayer;                        // BGM ����� AudioSource
    private float sfxVolume;
    private float bgmVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM("TestBGM");
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void Init()
    {
        // Dictionary �ʱ�ȭ
        soundDict = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }

        // BGM �÷��̾� �ʱ�ȭ
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;

        InitPool();
    }

    /// <summary>
    /// ������Ʈ Ǯ �ʱ�ȭ
    /// </summary>
    private void InitPool()
    {
        audioSourcePool = new Queue<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.enabled = false;
            audioSourcePool.Enqueue(source);
        }
    }

    /// <summary>
    /// SFX ��� (Object Pooling ���)    /// </summary>
    /// <param name="soundName">���� �̸�</param>
    public void PlaySFX(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            if (audioSourcePool.Count > 0)
            {
                AudioSource source = audioSourcePool.Dequeue();
                source.clip = clip;
                source.volume = sfxVolume;
                source.enabled = true;
                source.Play();

                StartCoroutine(ReturnToPool(source, clip.length));
            }
            else
            {
                // Ǯ�� ����� �ҽ��� ���� ���, ���� �����Ͽ� ���
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.clip = clip;
                newSource.volume = sfxVolume;
                newSource.playOnAwake = false;
                newSource.enabled = true;
                newSource.Play();

                // ���� ������ �ҽ��� ���� �� Ǯ�� �ٽ� ���� �� �ֵ��� �ڷ�ƾ�� ���
                StartCoroutine(ReturnToPool(newSource, clip.length));
            }
        }
        else
        {
            Debug.LogWarning("SFX not found");
        }
    }

    /// <summary>
    /// BGM ���
    /// </summary>
    /// <param name="bgmName">���� �̸�</param>
    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (bgmPlayer.clip != clip)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.volume = bgmVolume;
                bgmPlayer.Play();
            }
        }
        else
        {
            Debug.LogWarning("BGM not found");
        }
    }

    /// <summary>
    /// ��� ���� �ҽ� Ǯ�� �ǵ����� ����
    /// </summary>
    /// <param name="source"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.enabled = false;
        audioSourcePool.Enqueue(source);
    }

    /// <summary>
    /// SFX ���� ����
    /// </summary>
    /// <param name="volume">0~1 ������ ���� ��</param>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// BGM ���� ����
    /// </summary>
    /// <param name="volume">0~1 ������ ���� ��</param>
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmPlayer.volume = bgmVolume;
    }
}
