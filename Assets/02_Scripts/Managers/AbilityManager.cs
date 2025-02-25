using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AbilityRepositoy abilityRepositoy;
    public AbilityRepositoy AbilityRepositoy => abilityRepositoy;

    [SerializeField] private Transform abilityParent;

    private void Awake()
    {
        abilityParent = transform;
    }

    /// <summary>
    /// �������� ��� �����Ƽ �Ҹ�
    /// </summary>
    public void ClearOwnedAbilities()
    {
        AbilityRepositoy.ClearOwnedAbilities();
    }

    /// <summary>
    /// �ش� �����Ƽ ȹ�� �� ������Ʈ �����Ͽ� ������ ��ü ������ ����
    /// </summary>
    /// <param name="abilityEnum">�����Ƽ�� ID</param>
    public void SetAbility(AbilityEnum abilityEnum)
    {
        Debug.Log($"{abilityEnum}�޾Ѵ�");
        AbilityRepositoy.SetAbility(abilityEnum).transform.SetParent(abilityParent);
    }

    /// <summary>
    /// �������� �����Ƽ ���
    /// </summary>
    public void UseAbility()
    {
        foreach (AbilityController ability in AbilityRepositoy.GetOwnedAbilities())
        {
            ability.UseSkill();
        }
    }

    /// <summary>
    /// �������� �����Ƽ �߿��� ���ϴ� �����Ƽ ��ȭ
    /// </summary>
    /// <param name="abilityEnum">���ϴ� �����Ƽ�� ID</param>
    public void UpgradeOwnedAbility(AbilityEnum abilityEnum)
    {
        AbilityRepositoy.UpgradeOwnedAbility(abilityEnum);
    }

    /// <summary>
    /// ��� �����Ƽ �� ���ϴ� �����Ƽ�� AbilityData ã�Ƽ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="abilityEnum">���ϴ� �����Ƽ�� ID</param>
    /// <returns></returns>
    public AbilityData FindAbilityData(AbilityEnum abilityEnum)
    {
        return abilityRepositoy.FindAbilityData(abilityEnum);
    }
}
