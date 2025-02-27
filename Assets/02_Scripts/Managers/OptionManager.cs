using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

    [Header("���� ����")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI bgmValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;

    [Header("Ű ���ε� ����")]
    [SerializeField] private Button keyBindingButtonW;
    [SerializeField] private Button keyBindingButtonS;
    [SerializeField] private Button keyBindingButtonA;
    [SerializeField] private Button keyBindingButtonD;
    [SerializeField] private TextMeshProUGUI upBindingText;
    [SerializeField] private TextMeshProUGUI downBindingText;
    [SerializeField] private TextMeshProUGUI leftBindingText;
    [SerializeField] private TextMeshProUGUI rightBindingText;

    private Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

    private string waitingForKey = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ���� �����̴� ���� �̺�Ʈ ���
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Ű ���ε� ��ư �̺�Ʈ ���
        keyBindingButtonW.onClick.AddListener(() => StartKeyBinding("Up"));
        keyBindingButtonA.onClick.AddListener(() => StartKeyBinding("Left"));
        keyBindingButtonS.onClick.AddListener(() => StartKeyBinding("Down"));
        keyBindingButtonD.onClick.AddListener(() => StartKeyBinding("Right"));

        // �ʱⰪ ����
        bgmSlider.value = 1f;
        sfxSlider.value = 1f;
        keyBindings["Up"] = KeyCode.W;
        keyBindings["Left"] = KeyCode.A;
        keyBindings["Down"] = KeyCode.S;
        keyBindings["Right"] = KeyCode.D;

        UpdateVolumeUI();
        UpdateKeyBindingUI();
    }

    /// <summary>
    /// BGM ���� ����
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        SoundManager.instance.SetBGMVolume(volume);
        UpdateVolumeUI();
    }

    /// <summary>
    /// SFX ���� ����
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        SoundManager.instance.SetSFXVolume(volume);
        UpdateVolumeUI();
    }

    /// <summary>
    /// ���� ���� ���� UI�� ǥ��
    /// </summary>
    private void UpdateVolumeUI()
    {
        bgmValueText.text = Mathf.RoundToInt(bgmSlider.value * 100).ToString();
        sfxValueText.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
    }

    /// <summary>
    /// Ű ���ε� ���� ����
    /// </summary>
    private void StartKeyBinding(string key)
    {
        waitingForKey = key;
    }

    private void Update()
    {
        if (waitingForKey != null && Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    keyBindings[waitingForKey] = key;
                    waitingForKey = null;
                    Debug.Log($"Ű ���ε� ����: {key}");

                    // UI ������Ʈ
                    UpdateKeyBindingUI();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// ���� Ű ���ε��� UI�� ǥ��
    /// </summary>
    private void UpdateKeyBindingUI()
    {
        upBindingText.text = keyBindings["Up"].ToString();
        downBindingText.text = keyBindings["Down"].ToString();
        leftBindingText.text = keyBindings["Left"].ToString();
        rightBindingText.text = keyBindings["Right"].ToString();
    }

    /// <summary>
    /// ���ε��� Ű ��ȯ
    /// </summary>
    public KeyCode GetKey(string action)
    {
        return keyBindings.ContainsKey(action) ? keyBindings[action] : KeyCode.None;
    }
}