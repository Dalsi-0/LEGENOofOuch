using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUIController : MonoBehaviour
{
    // UI ��ҵ�
    [SerializeField] private RectTransform achievementUI;
    [SerializeField] private TextMeshProUGUI achievementNameText;
    [SerializeField] private TextMeshProUGUI achievementDescriptionText;
    [SerializeField] private Image achievementImage;
    [SerializeField] private Sprite[] iconlist;
    //[SerializeField] private TextMeshProUGUI[] achievementPanelName;
    [SerializeField] private TextMeshProUGUI[] achievementPanelDiscription;
    [SerializeField] private Image[] achievementPanelImage;
    public class AchievementData
    {
        public string name;
        public string description;
        public Sprite image;
    }

    // �������� ������ ����
    private Dictionary<string, AchievementData> achievements = new Dictionary<string, AchievementData>();

    private void Start()
    {
        // �������� ������ ���
        achievements.Add("LevelUp", new AchievementData { name = "ù ������!", description = "������ ó������ �÷Ƚ��ϴ�.", image = iconlist[0] });
        achievements.Add("Death", new AchievementData { name = "ù ���!", description = "ó������ ����Ͽ����ϴ�.", image = iconlist[1] });
        achievements.Add("Ability", new AchievementData { name = "ù ��ų ȹ��!", description = "���ο� �ɷ��� ������ϴ�.", image = iconlist[2] });
        achievements.Add("Trade", new AchievementData { name = "ù �ŷ�!", description = "�Ǹ��� ó������ �ŷ��߽��ϴ�.", image = iconlist[3] });
        achievements.Add("CastleClear", new AchievementData { name = "ù �� Ŭ����!", description = "ó������ ���� Ŭ�����߽��ϴ�.", image = iconlist[4] });
        achievements.Add("SwampClear", new AchievementData { name = "ù �� Ŭ����!", description = "ó������ ���� Ŭ�����߽��ϴ�.", image = iconlist[5] });
        achievements.Add("VolcanoClear", new AchievementData { name = "ù ȭ�� Ŭ����!", description = "ó������ ȭ���� Ŭ�����߽��ϴ�.", image = iconlist[6] });
    }

    private void OnEnable()
    {
        // �̺�Ʈ ���
        Achievements.OnFirstLevelUp += () => ShowAchievement("LevelUp");
        Achievements.OnFirstDeath += () => ShowAchievement("Death");
        Achievements.OnFirstAbility += () => ShowAchievement("Ability");
        Achievements.OnFirstTradlear += () => ShowAchievement("Trade");
        Achievements.OnFirstCastleClear += () => ShowAchievement("CastleClear");
        Achievements.OnFirstSwampClear += () => ShowAchievement("SwampClear");
        Achievements.OnFirstVolcanoClear += () => ShowAchievement("VolcanoClear");
        Achievements.OnFirstLevelUp += () => UpdatePannel("LevelUp");
        Achievements.OnFirstDeath += () => UpdatePannel("Death");
        Achievements.OnFirstAbility += () => UpdatePannel("Ability");
        Achievements.OnFirstTradlear += () => UpdatePannel("Trade");
        Achievements.OnFirstCastleClear += () => UpdatePannel("CastleClear");
        Achievements.OnFirstSwampClear += () => UpdatePannel("SwampClear");
        Achievements.OnFirstVolcanoClear += () => UpdatePannel("VolcanoClear");

    }

    private void OnDisable()
    {
        // �̺�Ʈ ����
        Achievements.OnFirstLevelUp -= () => ShowAchievement("LevelUp");
        Achievements.OnFirstDeath -= () => ShowAchievement("Death");
        Achievements.OnFirstAbility -= () => ShowAchievement("Ability");
        Achievements.OnFirstTradlear -= () => ShowAchievement("Trade");
        Achievements.OnFirstCastleClear -= () => ShowAchievement("CastleClear");
        Achievements.OnFirstSwampClear -= () => ShowAchievement("SwampClear");
        Achievements.OnFirstVolcanoClear -= () => ShowAchievement("VolcanoClear");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("LevelUp");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("Death");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("Ability");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("Trade");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("CastleClear");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("SwampClear");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("VolcanoClear");
    }

    /// <summary>
    /// �������� UI�� ǥ���ϴ� �޼���
    /// </summary>
    private void ShowAchievement(string achievementKey)
    {
        if (!achievements.ContainsKey(achievementKey))
        {
            Debug.LogWarning($"�������� �����Ͱ� �������� �ʽ��ϴ�: {achievementKey}");
            return;
        }

        AchievementData achievement = achievements[achievementKey];

        // UI ��� ������Ʈ
        achievementNameText.text = achievement.name;
        achievementDescriptionText.text = achievement.description;
        achievementImage.sprite = achievement.image;

        // UI �ִϸ��̼� ����


        float targetY = achievementUI.transform.localPosition.y + 180f;
        float originalY = achievementUI.transform.localPosition.y; // ���� ��ġ ����

        Sequence sequence = DOTween.Sequence();
        sequence.Append(achievementUI.transform.DOLocalMoveY(targetY, 0.5f).SetEase(Ease.OutCubic)) // 360 ���� �̵�
                .AppendInterval(1f) // 1�� ���
                .Append(achievementUI.transform.DOLocalMoveY(originalY, 0.5f).SetEase(Ease.InCubic)); // �ٽ� ���� ��ġ�� �̵�
    }
    private void UpdatePannel(string achievementKey)
    {
        if (!achievements.ContainsKey(achievementKey))
        {
            Debug.LogWarning($"�������� �����Ͱ� �������� �ʽ��ϴ�: {achievementKey}");
            return;
        }
        switch (achievementKey)         
        {
            case "LevelUp":
                UpdateUIData(achievementKey, 0);
                break;
            case "Death":
                UpdateUIData(achievementKey, 1);
                break;
            case "Ability":
                UpdateUIData(achievementKey, 2);
                break;
            case "Trade":
                UpdateUIData(achievementKey, 3);
                break;
            case "CastleClear":
                UpdateUIData(achievementKey, 4);
                break;
            case "SwampClear":
                UpdateUIData(achievementKey, 5);
                break;
            case "VolcanoClear":
                UpdateUIData(achievementKey, 6);
                break;
        }
    }
    private void UpdateUIData(string achievementKey,int index)
    {
        AchievementData achievement = achievements[achievementKey];
        //achievementPanelName[index].text = achievement.name;
        achievementPanelDiscription[index].text = achievement.description;
        achievementPanelImage[index].sprite = achievement.image;
    }

}