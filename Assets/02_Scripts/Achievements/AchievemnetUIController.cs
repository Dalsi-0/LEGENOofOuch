using UnityEngine;

public class AchievementUIController : MonoBehaviour
{
    // Achievement UI�� RectTransform�� �ν����Ϳ��� ���� (�ʱ� Y���� -180���� ����)
    [SerializeField] private RectTransform achievementUI;

    private void OnEnable()
    {
        Achievements.OnFirstLevelUp += GetAchievemeent;
        Achievements.OnFirstDeath += GetAchievemeent;
        Achievements.OnFirstRoundClear += GetAchievemeent;
        Achievements.OnFirstAbility += GetAchievemeent;
        Achievements.OnFirstTradlear += GetAchievemeent;
        Achievements.OnFirstCastleClear += GetAchievemeent;
        Achievements.OnFirstSwampClear += GetAchievemeent;
        Achievements.OnFirstVolcanoClear += GetAchievemeent;
}

    private void OnDisable()
    {
        Achievements.OnFirstRoundClear -= GetAchievemeent;

    }

    // �̺�Ʈ�� �߻��ϸ� Achievement UI�� Y���� -180���� 180���� ����
    private void GetAchievemeent()
    {

    }
}