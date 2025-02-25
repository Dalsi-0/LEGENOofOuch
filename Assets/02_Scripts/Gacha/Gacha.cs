using System;
using UnityEngine;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEditor.Searcher;

public class Gacha : MonoBehaviour
{
    public int abilityindex = Enum.GetValues(typeof(AbilityEnum)).Length;
    private AbilityEnum[] selectedAbility = new AbilityEnum[3]; // ���õ� 3�� �ɷ� �ε���
    public bool isRare = false;

    /// <summary>
    /// �ɷ��� �������� ����
    /// 5���� Ȯ���� ���� �ɷ��� ���õ�
    /// �ڽ��� ������ �´� �ɷ¸� ����
    /// </summary>
    public void SelectRandomAbility()
    {
        // Rare�� �з��� �ε��� ����
        AbilityEnum[] rareIndices = new AbilityEnum[] {
        AbilityEnum.BloodThirst, AbilityEnum.Invincibility, AbilityEnum.Blaze,
        AbilityEnum.Spirit, AbilityEnum.Archer, AbilityEnum.Mage, AbilityEnum.Warrior
    };
        AbilityEnum DevilIndices = AbilityEnum.ExtraLife;
        AbilityEnum[] sourceIndices;

        // IsRare()�� ���� �ɷ� ���� ����
        IsRare();

        // �÷��̾��� ������ ������
        PlayerClassEnum playerClass = PlayerClassEnum.Archer;
        //�ϵ��ڵ��� ���߿� �����Ұ�

        if (isRare)
        {
            // ���� �ɷ¸� ����
            sourceIndices = rareIndices;
        }
        else
        {
            List<AbilityEnum> nonRareList = new List<AbilityEnum>();

            // ������ ���� �ʴ� �ɷµ��� ����
            foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
            {
                if (Array.IndexOf(rareIndices, ability) < 0 && ability != DevilIndices && IsAbilityValidForClass(ability, playerClass))
                {
                    nonRareList.Add(ability);
                }
            }

            // �Ϲ� �ɷ� ����� sourceIndices�� �Ҵ�
            sourceIndices = nonRareList.ToArray();
        }

        for (int i = sourceIndices.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            AbilityEnum temp = sourceIndices[i];
            sourceIndices[i] = sourceIndices[randomIndex];
            sourceIndices[randomIndex] = temp;
        }

        for (int i = 0; i < selectedAbility.Length; i++)
        {
            selectedAbility[i] = sourceIndices[i];
            Debug.Log($"���õ� �ɷ� {i} : {selectedAbility[i]}");
        }
    }

    /// <summary>
    /// ������ �´� �ɷ¸� �����ϵ��� ���͸�
    /// </summary>
    private bool IsAbilityValidForClass(AbilityEnum ability, PlayerClassEnum playerClass)
    {
        switch (playerClass)
        {
            case PlayerClassEnum.Warrior:
                return ability != AbilityEnum.Archer && ability != AbilityEnum.Mage;
            case PlayerClassEnum.Archer:
                return ability != AbilityEnum.Warrior && ability != AbilityEnum.Mage;
            case PlayerClassEnum.Mage:
                return ability != AbilityEnum.Warrior && ability != AbilityEnum.Archer;
            default:
                return true; // ��� ������ ������ �� �ִ� ��� (�⺻��)
        }
    }

    /// <summary>
    /// ���Ӹ޴������� ���õ� �ɷ��� ����
    /// </summary>
    /// <returns></returns>
    public AbilityEnum[] GetSelectedAbility()
    {
        return selectedAbility;
    }

    /// <summary>
    /// ���� �ɷ��� ���õǾ����� ���� ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool GetIsRare()
    {
        return isRare;
    }

    /// <summary>
    /// Ȯ���� ���� ���� �ɷ��� ����
    /// </summary>
    public void IsRare()
    {
        if (UnityEngine.Random.Range(0, 100) < 5)
            isRare = true;
    }

    public AbilityEnum[] GachaSelect()
    {
        return selectedAbility;
    }
}