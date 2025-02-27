using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AbilityRepositoy abilityRepositoy;
    public AbilityRepositoy AbilityRepositoy => abilityRepositoy;

    [SerializeField] private Transform abilityParent;
    private bool multiShotOn = false;

    /// <summary>
    /// �����Ƽ ������ ���̶���Ű ������ ���� �θ� ����
    /// </summary>
    private void Start()
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
        GameObject abilityObject = AbilityRepositoy.SetAbility(abilityEnum);

        if (abilityObject != null)
        {
            abilityObject.transform.SetParent(abilityParent);
        }
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
    public AbilityDataSO FindAbilityData(AbilityEnum abilityEnum)
    {
        return abilityRepositoy.FindAbilityData(abilityEnum);
    }

    /// <summary>
    /// �����Ƽ �� ��Ƽ�� ��� On/Off
    /// </summary>
    /// <param name="value"></param>
    public void SetMultiShotOn(bool value)
    {
        multiShotOn = value;
    }
    public bool GetMultiShotOn()
    {
        return multiShotOn;
    }
}
