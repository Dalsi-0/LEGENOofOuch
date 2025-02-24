// Gacha.cs
using UnityEngine;

public class Gacha : MonoBehaviour
{
    public Sprite[] skillIcons; // ��� ������ ��ų ������ �迭
    private Sprite[] selectedIcons = new Sprite[3]; // ���õ� 3�� ������

    public void SelectRandomIcons()
    {
        for (int i = 0; i < 3; i++)
        {
            selectedIcons[i] = skillIcons[Random.Range(0, skillIcons.Length)];
        }
    }

    public Sprite[] GetSelectedIcons()
    {
        return selectedIcons;
    }

    public bool IsRare(Sprite icon)
    {
        int iconIndex = System.Array.IndexOf(skillIcons, icon);
        return iconIndex >= 26 && iconIndex <= 30;
    }
}
