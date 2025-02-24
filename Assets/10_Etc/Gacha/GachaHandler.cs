using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Experimental.GlobalIllumination;

public class GachaHandler : MonoBehaviour
{
    public Gacha gacha = GachaManager.Instance.gacha;
    public GachaAnimation[] gachaAnimation;
    public RectTransform[] skillSlots;
    public Material pillarMaterial; // ��� ��Ƽ����
    public Sprite[] skillIcons;
    public GameObject Piller;
    public GameObject commonBackground;
    public GameObject rareBackground;
    public float bounceScale = 1.2f;
    public float bounceDuration = 0.2f;

    // ���� ����
    private Color commonColor = Color.green;  // �⺻ �ʷϻ�
    private Color rareColor = Color.yellow;   // ���� Ȯ���� �� �����


    public void StartGacha()
    {
        StartCoroutine(HandleGacha());
    }
    private IEnumerator HandleGacha()
    {
        //yield return StartCoroutine(gachaAnimation.AnimateSlot());

        Piller.SetActive(true);
        ChangePillarColor(commonColor);
        int[] selectedAbility = gacha.GetSelectedAbility();
        bool isRare = gacha.GetIsRare();
        for (int i = 0; i < selectedAbility.Length; i++)
        {
            gachaAnimation[i].StartSpin(selectedAbility[i], isRare);
        }
        // 2�� �� ���� ����
        

        yield return new WaitForSeconds(2f);
        if (!isRare)
        {
            for (int i = 0; i < selectedAbility.Length; i++)
            {
                StartCoroutine(PlayBounceEffect(skillSlots[i]));
                yield return new WaitForSeconds(0.5f);
            }
            Piller.SetActive(false);
            commonBackground.SetActive(true);
        }
        else
        {
            ChangePillarColor(rareColor);
            for (int i = 0; i < selectedAbility.Length; i++)
            {
                StartCoroutine(PlayBounceEffect(skillSlots[i]));
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < selectedAbility.Length; i++)
            {
                StartCoroutine(PlayBounceEffect(skillSlots[i]));
                yield return new WaitForSeconds(0.5f);
            }
            Piller.SetActive(false);
            rareBackground.SetActive(true);
        }
        

    }
    private IEnumerator PlayBounceEffect(RectTransform slot)
    {
        Vector3 originalScale = slot.localScale;
        Vector3 targetScale = originalScale * bounceScale;
        float elapsedTime = 0f;

        while (elapsedTime < bounceDuration)
        {
            elapsedTime += Time.deltaTime;
            slot.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / bounceDuration);
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < bounceDuration)
        {
            elapsedTime += Time.deltaTime;
            slot.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / bounceDuration);
            yield return null;
        }
    }

    private void ChangePillarColor(Color color)
    {
            pillarMaterial.SetColor("_Color", color);
    }
}
