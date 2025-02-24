
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GachaAnimation : MonoBehaviour
{
    public Image slotImage;
    public Sprite[] skillIcons;
    public float spinDuration = 2f;
    public float commonSpinDuration = 2f;
    public float rareSpinDuration = 3f;
    public float initialDelay = 0.05f;
    public float finalDelay = 0.3f;

    // Rare�� ������ �ε��� ����
    private int[] rareIndices = new int[] { 7, 8, 16, 19, 21, 22, 23 };
    public Color commonColor = Color.green;
    public Color rareColor = Color.yellow;

    void Awake()
    {
        // ����: ���� GameObject�� GachaHandler�� �پ��ִٰ� ������ ��
        GachaHandler handler = GetComponent<GachaHandler>();

    }
    /// <summary>
    /// �ܺο��� �ִϸ��̼��� ������ �� ȣ��.
    /// selectedAbility: ���������� ������ �ɷ� �ε���.
    /// isRare: �����̸� true, �ƴϸ� false.
    /// </summary>
    public void StartSpin(AbilityEnum selectedAbility, bool isRare)
    {
        StartCoroutine(AnimateSlot(selectedAbility, isRare));
    }

    private IEnumerator AnimateSlot(AbilityEnum selectedAbility, bool isRare)
    {
        // ���� ������, �� �ܰ�(�Ϲ� �� ����)�� ����
        if (isRare)
        {
            List<Sprite> nonRareSprites = new List<Sprite>();
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (System.Array.IndexOf(rareIndices, i) < 0)
                    nonRareSprites.Add(skillIcons[i]);
            }
            Sprite[] commonSprites = nonRareSprites.ToArray();
            if (commonSprites.Length == 0)
                yield break;
            
            int currentIndex = Random.Range(0, commonSprites.Length);
            float elapsed = 0f;
            while (elapsed < commonSpinDuration)
            {
                slotImage.sprite = commonSprites[currentIndex];
                currentIndex = (currentIndex + 1) % commonSprites.Length;
                float t = elapsed / commonSpinDuration;
                float currentDelay = Mathf.Lerp(initialDelay, initialDelay, t);
                yield return new WaitForSeconds(currentDelay);
                elapsed += currentDelay;
            }

            List<Sprite> rareSprites = new List<Sprite>();
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (System.Array.IndexOf(rareIndices, i) >= 0)
                    rareSprites.Add(skillIcons[i]);
            }
            Sprite[] rareSpriteArray = rareSprites.ToArray();
            if (rareSpriteArray.Length == 0)
                yield break;

            currentIndex = Random.Range(0, rareSpriteArray.Length);
            elapsed = 0f;
            while (elapsed < rareSpinDuration)
            {
                slotImage.sprite = rareSpriteArray[currentIndex];
                currentIndex = (currentIndex + 1) % rareSpriteArray.Length;
                float t = elapsed / rareSpinDuration;
                float currentDelay = Mathf.Lerp(initialDelay, finalDelay, t);
                yield return new WaitForSeconds(currentDelay);
                elapsed += currentDelay;
            }
        }
        else
        {
            List<Sprite> nonRareSprites = new List<Sprite>();
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (System.Array.IndexOf(rareIndices, i) < 0)
                    nonRareSprites.Add(skillIcons[i]);
            }
            Sprite[] commonSprites = nonRareSprites.ToArray();
            if (commonSprites.Length == 0)
                yield break;

            int currentIndex = Random.Range(0, commonSprites.Length);
            float elapsed = 0f;
            while (elapsed < spinDuration)
            {
                slotImage.sprite = commonSprites[currentIndex];
                currentIndex = (currentIndex + 1) % commonSprites.Length;
                float t = elapsed / spinDuration;
                float currentDelay = Mathf.Lerp(initialDelay, finalDelay, t);
                yield return new WaitForSeconds(currentDelay);
                elapsed += currentDelay;
            }
        }

        // ���� ���: selectedAbility ���� �ش��ϴ� ��������Ʈ�� ����
        // (�̶� selectedAbility�� ��ü �迭(AbilityIcons)���� ��ȿ�� �ε������� ��)
        if ((int)selectedAbility < 0 || (int)selectedAbility >= skillIcons.Length)
        {
            Debug.LogWarning("selectedAbility out of range. Using index 0.");
            selectedAbility = 0;
        }
        slotImage.sprite = skillIcons[(int)selectedAbility];
    }
}

