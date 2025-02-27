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
    public GachaAbilityController gachaAbilityController;//�ɷ� ���׷��̵� ��ġ�� �����ϴ� Ŭ����
   
    /// <summary>
    /// �ɷ��� �������� ����
    /// 5���� Ȯ���� ���� �ɷ��� ���õ�
    /// �ڽ��� ������ �´� �ɷ¸� ����
    /// �̹� Ǯ���׷��̵�� �ĺ�Ǯ���� ����
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
        PlayerClassEnum playerClass = GameManager.Instance.playerClassEnum;

        if (isRare)
        {
            List<AbilityEnum> validRareList = new List<AbilityEnum>();
            foreach (AbilityEnum ability in rareIndices)
            {
                if (IsAbilityValidForClass(ability, playerClass))
                {
                    validRareList.Add(ability);
                }
            }
            // ���� �ɷ¸� ����

            sourceIndices = validRareList.ToArray();
        }
        else
        {
            List<AbilityEnum> nonRareList = new List<AbilityEnum>();

            // ������ ���� �ʴ� �ɷµ��� ����
            foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
            {
                if (Array.IndexOf(rareIndices, ability) < 0 && ability != DevilIndices)
                {
                    nonRareList.Add(ability);
                }
            }

            // �Ϲ� �ɷ� ����� sourceIndices�� �Ҵ�
            sourceIndices = nonRareList.ToArray();
        }

        List<AbilityEnum> candidatePool = new List<AbilityEnum>(sourceIndices);
        for (int i = candidatePool.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            AbilityEnum temp = candidatePool[i];
            candidatePool[i] = candidatePool[randomIndex];
            candidatePool[randomIndex] = temp;
        }

        // �� ���Կ� ���� �ɷ��� ���� (�ߺ� ����)
        for (int i = 0; i < selectedAbility.Length; i++)
        {
            bool candidateFound = false;
            AbilityEnum selectedCandidate = default;
            // �ĺ� Ǯ���� Ǯ���׷��̵尡 �ƴ� �ɷ��� ã�� ����
            for (int j = 0; j < candidatePool.Count; j++)
            {
                if (!gachaAbilityController.FullUpgrade(candidatePool[j]))
                {
                    selectedCandidate = candidatePool[j];
                    candidatePool.RemoveAt(j); // ������ �ĺ��� �ߺ� ������ ���� ����
                    candidateFound = true;
                    break;
                }
            }
            // ���� �ĺ� Ǯ�� ���� ��� �ɷ��� �̹� Ǯ���׷��̵���,
            // �Ǵ� �ĺ� Ǯ�� ����ִٸ�, �ʿ信 ���� �⺻�� ó�� �Ǵ� ��� ���
            if (!candidateFound)
            {
                if (candidatePool.Count > 0)
                {
                    selectedCandidate = candidatePool[0];
                    candidatePool.RemoveAt(0);
                }
                else
                {
                    continue;
                }
            }
            selectedAbility[i] = selectedCandidate;
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
    /// Ȯ���� ���� ���� �ɷ��� ����//test�� 15��
    /// </summary>
    public void IsRare()
    {
        if (UnityEngine.Random.Range(0, 100) <15)
            isRare = true;
    }

    /// <summary>
    /// ��í���� ������ �ɷ��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public AbilityEnum[] GachaSelect()
    {
        return selectedAbility;
    }
}