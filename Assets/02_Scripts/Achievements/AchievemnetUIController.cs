using UnityEngine;

public class AchievementUIController : MonoBehaviour
{
    // Achievement UI�� RectTransform�� �ν����Ϳ��� ���� (�ʱ� Y���� -180���� ����)
    [SerializeField] private RectTransform achievementUI;

    private void OnEnable()
    {
        // ���÷� ù ���� Ŭ���� �̺�Ʈ�� �߻��ϸ� UI�� ������Ʈ�ϵ��� ����
        Achievements.OnFirstRoundClear += HandleFirstRoundClear;
    }

    private void OnDisable()
    {
        Achievements.OnFirstRoundClear -= HandleFirstRoundClear;
    }

    // �̺�Ʈ�� �߻��ϸ� Achievement UI�� Y���� -180���� 180���� ����
    private void HandleFirstRoundClear()
    {
        if (achievementUI != null)
        {
            Vector2 pos = achievementUI.anchoredPosition;
            pos.y = 180;
            achievementUI.anchoredPosition = pos;
            Debug.Log("Achievement UI�� Y���� 180���� ����Ǿ����ϴ�.");
        }
    }
}