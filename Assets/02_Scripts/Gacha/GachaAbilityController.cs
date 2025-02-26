using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GachaAbilityController : MonoBehaviour
{

    private int[] upgradeCounts;

    private void Awake()
    {
        int count = Enum.GetValues(typeof(AbilityEnum)).Length;
        upgradeCounts = new int[count];
    }

    /// <summary>
    /// ������ �ɷ��� ���׷��̵� Ƚ���� 1 ������ŵ�ϴ�.
    /// </summary>
    /// <param name="ability">���׷��̵��� �ɷ�</param>
    public void UpgradeAbility(AbilityEnum ability)
    {
        upgradeCounts[(int)ability]++;
    }

    /// <summary>
    /// ������ �ɷ��� ���� ���׷��̵� Ƚ���� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="ability">��ȸ�� �ɷ�</param>
    /// <returns>���׷��̵� Ƚ��</returns>
    public int GetUpgradeCount(AbilityEnum ability)
    {
        return upgradeCounts[(int)ability];
    }

    public bool FullUpgrade(AbilityEnum ability)
    {
        if (upgradeCounts[(int)ability] == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ClearUpgradeCount()
    {
        for (int i = 0; i < upgradeCounts.Length; i++)
        {
            upgradeCounts[i] = 0;
        }
    }
}