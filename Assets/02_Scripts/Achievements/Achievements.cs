using System;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    // �� ���������� ���� (static���� �����Ͽ� ���� ���·� ����)
    public static bool isFirstLevelUp { get; private set; } = false;
    public static bool isFirstDeath { get; private set; } = false;
    public static bool isFirstAbility { get; private set; } = false;
    public static bool isFirstTradlear { get; private set; } = false;
    public static bool isFirstCastleClear { get; private set; } = false;
    public static bool isFirstSwampClear { get; private set; } = false;
    public static bool isFirstVolcanoClear { get; private set; } = false;

    // �ܹ߼� �̺�Ʈ (�� �������� �߻� �� �� ���� ȣ��)
    public static event Action OnFirstLevelUp;
    public static event Action OnFirstDeath;
    public static event Action OnFirstAbility;//�߰��Ϸ�
    public static event Action OnFirstTradlear;
    public static event Action OnFirstCastleClear;
    public static event Action OnFirstSwampClear;
    public static event Action OnFirstVolcanoClear;

 
    
    /// <summary>
    /// �������� Ʈ���� �޼����
    /// </summary>
    public static void TriggerFirstLevelUp()
    {
        if (!isFirstLevelUp)
        {
            isFirstLevelUp = true;
            OnFirstLevelUp?.Invoke();
        }
    }

    public static void TriggerFirstDeath()
    {
        if (!isFirstDeath)
        {
            isFirstDeath = true;
            OnFirstDeath?.Invoke();
        }
    }


    public static void TriggerFirstAbility()
    {
        if (!isFirstAbility)
        {
            isFirstAbility = true;
            OnFirstAbility?.Invoke();
        }
    }

    public static void TriggerFirstTradlear()
    {
        if (!isFirstTradlear)
        {
            isFirstTradlear = true;
            OnFirstTradlear?.Invoke();
        }
    }

    public static void TriggerFirstCastleClear()
    {
        if (!isFirstCastleClear)
        {
            isFirstCastleClear = true;
            OnFirstCastleClear?.Invoke();
        }
    }

    public static void TriggerFirstSwampClear()
    {
        if (!isFirstSwampClear)
        {
            isFirstSwampClear = true;
            OnFirstSwampClear?.Invoke();
        }
    }

    public static void TriggerFirstVolcanoClear()
    {
        if (!isFirstVolcanoClear)
        {
            isFirstVolcanoClear = true;
            OnFirstVolcanoClear?.Invoke();
        }
    }
}
