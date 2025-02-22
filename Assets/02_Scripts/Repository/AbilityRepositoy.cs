using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class AbilityRepositoy : MonoBehaviour
{
    private List<AbilityController> ownedAbilities = new List<AbilityController>();

    private Dictionary<AbilityEnum, GameObject> dicAbilityPrefabs = new Dictionary<AbilityEnum, GameObject>();
    private Dictionary<AbilityEnum, AbilityDataSO> dicAbilityDataSO = new Dictionary<AbilityEnum, AbilityDataSO>();

    [SerializeField] private AbilityDataSO[] abilityDataSOs;
    [SerializeField] private GameObject[] abilityPrefabs;

    private void Awake()
    {
        InitDictionary();
    }

    /// <summary>
    /// dicAbilityPrefabs, dicAbilityDataSO�ʱ�ȭ
    /// </summary>
    private void InitDictionary()
    {
        dicAbilityPrefabs.Clear();
        dicAbilityDataSO.Clear();

        for (int i = 0; i < abilityDataSOs.Length; i++)
        {
            dicAbilityPrefabs[abilityDataSOs[i].Ability] = abilityPrefabs[i];
            dicAbilityDataSO[abilityDataSOs[i].Ability] = abilityDataSOs[i];
        }
    }

    /// <summary>
    /// �������� ��� �����Ƽ �Ҹ�
    /// </summary>
    public void ClearOwnedAbilities()
    {
        for (int i = ownedAbilities.Count - 1; i >= 0; i--)
        {
            Destroy(ownedAbilities[i].gameObject);
            ownedAbilities.RemoveAt(i); 
        }
    }

    /// <summary>
    /// �ش� �����Ƽ�� Dic���� �˻� �� ������Ʈ �����Ͽ� ���� �����Ƽ ��Ͽ� �߰�
    /// </summary>
    /// <param name="ability">�����Ƽ�� ID</param>
    /// <returns>������ �����Ƽ ������Ʈ</returns>
    public GameObject SetAbility(AbilityEnum ability)
    {
        GameObject abilityPrefab = Instantiate(dicAbilityPrefabs[ability]);
        ownedAbilities.Add(abilityPrefab.transform.GetComponent<AbilityController>());

        abilityPrefab.transform.GetComponent<AbilityController>().Init(dicAbilityDataSO[ability]);

        return abilityPrefab;
    }

    /// <summary>
    /// ����Ʈ ���� ���� ReadOnlyCollection ��ȯ, ���� �Ұ�
    /// </summary>
    public ReadOnlyCollection<AbilityController> GetOwnedAbilities()
    {
        return ownedAbilities.AsReadOnly();
    }

    /// <summary>
    /// �������� �����Ƽ ���� �ش� �����Ƽ ��ȭ
    /// </summary>
    /// <param name="ability"></param>
    public void UpgradeOwnedAbility(AbilityEnum ability)
    {
        foreach (var abilityController in ownedAbilities)
        {
            if (abilityController.AbilityBase.abilityData.abilityID == ability)
            {
                abilityController.AbilityBase.UpgradeAbility();
                return;
            }
        }
        Debug.Log($"{ability}�� ����");
    }


    /// <summary>
    /// ��� �����Ƽ �� ���ϴ� �����Ƽ�� AbilityData ã�Ƽ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="abilityEnum">���ϴ� �����Ƽ�� ID</param>
    /// <returns></returns>
    public AbilityData FindAbilityData(AbilityEnum abilityEnum)
    {
        return dicAbilityPrefabs[abilityEnum].GetComponent<AbilityController>().AbilityBase.abilityData;
    }
}
